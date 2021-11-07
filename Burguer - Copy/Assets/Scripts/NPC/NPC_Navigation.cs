using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Z_utilites.ProbabilitySystem;

namespace Entities.NPC
{
    public class NPC_Navigation : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------		
        public bool arrivedToDestination;
        [Tooltip("In percentages")]
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        NavMeshAgent agent;
        DestinationPoints destinationObject;
        GameObject destini; // for debug
        int randomTypeOfPoint; // 0 is for finish points (inside the restaurant), 1 is for dead zones (to not enter the restaurant)
        int randomDestination;
        CompareFood compareFood;
        NPC_DialogueBehaviour dialogue;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            compareFood = GetComponent<CompareFood>();
            dialogue = GetComponentInChildren<NPC_DialogueBehaviour>();
        }

        private void OnEnable()
        {
            agent = this.GetComponent<NavMeshAgent>();
            destinationObject = GameObject.FindGameObjectWithTag("Destination").GetComponent<DestinationPoints>();
            CalculateDestination();
            destinationObject.on_NPC_ArrivedToADestination += CheckIfPointIsStillAvailable;
            compareFood.onIngredientsChecked += ToGetOutWhenQuenchied;
        }

        private void OnDisable()
        {
            destinationObject.on_NPC_ArrivedToADestination -= CheckIfPointIsStillAvailable;
            compareFood.onIngredientsChecked -= ToGetOutWhenQuenchied;
        }

        private void Update()
        {
            if (destini == null)
                CalculateDestination();
        }

        void CalculateDestination()
        {
            if (destinationObject == null)
            {
                Debug.Log("Pasó");
                destinationObject = GameObject.FindGameObjectWithTag("Destination").GetComponent<DestinationPoints>();
            }
            if (Probability.ProbabilityCheck(destinationObject.probabilityNpcEnterToRestaurant))
            {
                randomTypeOfPoint = 0; // to choose among the points inside the restaurant (to enter NPC)
            }
            else
            {
                randomTypeOfPoint = 1;// to choose among the points outside the restaurant (to npc give a fuck the restaurant)
            }
            randomDestination = Random.Range(0, destinationObject.destinationPoints[randomTypeOfPoint].Count);

            if (randomTypeOfPoint == 0)// to check if a finish point is alredy taked
            {
                if (destinationObject.CheckIfAlredyInUse(randomDestination) == false)
                {
                    agent.SetDestination(destinationObject.destinationPoints[0][randomDestination].position);
                    destini = destinationObject.destinationPoints[0][randomDestination].gameObject;
                }
            }
            else
            {
                agent.SetDestination(destinationObject.destinationPoints[1][randomDestination].position);
                destini = destinationObject.destinationPoints[1][randomDestination].gameObject;
            }
        }

        void CheckIfPointIsStillAvailable()
        {
            // to recalculate a new location in case an NPC is alredy there
            if (randomTypeOfPoint == 0)
            {
                if (destinationObject.CheckIfAlredyInUse(randomDestination) && arrivedToDestination == false)
                {
                    CalculateDestination();
                }
            }
        }

        void ToGetOutWhenQuenchied()
        {
            randomDestination = Random.Range(0, destinationObject.destinationPoints[1].Count);
            agent.SetDestination(destinationObject.destinationPoints[1][randomDestination].position);
        }

        bool counter = true;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Finish") && counter)
            {
                arrivedToDestination = true;
                destinationObject.MarkPointAsAlredyUsed(randomDestination);
                counter = false;
                dialogue.canAppear = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals("Finish") && counter == false)
            {
                destinationObject.UnmarkPointAsAlredyUsed(randomDestination);
                counter = true;
                dialogue.canAppear = false;
            }
        }
        #endregion
    }
}