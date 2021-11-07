using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    public class DeathZoneForNPC : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        public SO.Variables.SO_IntVariable numberOfNpc;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        #endregion

        #region methods ---------------------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("NPC"))
            {
                numberOfNpc.ChangeCurrentValueBy(-1);
                Destroy(other.gameObject);
            }
        }
        #endregion
    }
}