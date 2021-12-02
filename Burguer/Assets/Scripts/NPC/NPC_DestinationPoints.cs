using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z_utilites.ProbabilitySystem;

namespace Entities.NPC
{
    public class NPC_DestinationPoints : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        public List<Transform> destinationsInRestaurant = new List<Transform>();
        public List<Transform> destinationsInDeathZones = new List<Transform>();
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            foreach (Transform child in this.transform)
            {
                if(child.gameObject.tag.Equals("Destiny point"))
                    destinationsInRestaurant.Add(child);
                else
                    destinationsInDeathZones.Add(child);
            }
        }
        #endregion
    }
}