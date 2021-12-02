using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z_utilites.ProbabilitySystem;
using UnityEngine.AI;

namespace Entities.NPC
{
    public class NPC_Navigation : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------	             
        public int enterToRestaurantProbability = 30;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        NPC_DestinationPoints destinationObject;
        NavMeshAgent agent;
        IndividualDestiniPoint obtainedDestinyPoint;
        CompareFood compareFood;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            destinationObject = GameObject.FindGameObjectWithTag("Destination").GetComponent<NPC_DestinationPoints>();
            agent = GetComponent<NavMeshAgent>();
            compareFood = GetComponent<CompareFood>();
        }

        private void OnEnable()
        {
            compareFood.onIngredientsChecked += GetOutFromTheRestaurant;
            // to make the npc go to the restaurant or ignore it randomly
            if (Probability.ProbabilityCheck(enterToRestaurantProbability))
            {
                obtainedDestinyPoint = destinationObject.destinationsInRestaurant[Random.Range(0, destinationObject.destinationsInRestaurant.Count)].GetComponent<IndividualDestiniPoint>();
                if (obtainedDestinyPoint.pointAlredyInUse == false)
                {
                    obtainedDestinyPoint.pointAlredyInUse = true;
                    agent.SetDestination(obtainedDestinyPoint.transform.position);
                }
                else
                    agent.SetDestination(destinationObject.destinationsInDeathZones[Random.Range(0, destinationObject.destinationsInDeathZones.Count)].transform.position);
            }
            else
                agent.SetDestination(destinationObject.destinationsInDeathZones[Random.Range(0, destinationObject.destinationsInDeathZones.Count)].transform.position);
        }

        private void OnDisable()
        {
            compareFood.onIngredientsChecked -= GetOutFromTheRestaurant;
        }

        void GetOutFromTheRestaurant()
        {
            obtainedDestinyPoint.pointAlredyInUse = false;
            agent.SetDestination(destinationObject.destinationsInDeathZones[Random.Range(0, destinationObject.destinationsInDeathZones.Count)].transform.position);
        }
        #endregion
    }
}