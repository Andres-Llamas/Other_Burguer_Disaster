using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    public class DestinationPoints : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        public int probabilityNpcEnterToRestaurant = 30;        
        #endregion

        #region NO_Inspector variables ------------------------------------------------	
        public List<List<Transform>> destinationPoints = new List<List<Transform>>();// 0 = Finish (inside restaurant), 1= dead zone (outside the restaurant)
        List<Transform> finishPoints = new List<Transform>();
        List<Transform> deadZonePoints = new List<Transform>();
        public delegate void Notify();
        public event Notify on_NPC_ArrivedToADestination;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            foreach (Transform point in this.transform)
            {
                if(point.gameObject.tag.Equals("Finish"))
                    finishPoints.Add(point);
                else
                    deadZonePoints.Add(point);
            }

            destinationPoints.Add(finishPoints);
            destinationPoints.Add(deadZonePoints);
        }

        /// <summary>
        /// To mark a Finish point as alredy used to avoid more NPC get to the same point         
        /// </summary>
        /// <param name="index"></param>
        public void MarkPointAsAlredyUsed(int index)
        {            
            destinationPoints[0][index].GetComponent<IndividualDestinationPoint>().pointAlredyInUse = true;
            on_NPC_ArrivedToADestination.Invoke();
        }

        public void UnmarkPointAsAlredyUsed(int index)
        {
            destinationPoints[0][index].GetComponent<IndividualDestinationPoint>().pointAlredyInUse = false;
        }

        public bool CheckIfAlredyInUse(int index)
        {            
            return destinationPoints[0][index].GetComponent<IndividualDestinationPoint>().pointAlredyInUse;            
        }
        #endregion
    }
}