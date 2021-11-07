using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    public class NPC_DialogueBehaviour : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------        
		public Animator anim;
        public bool canAppear;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        bool counter = true;
        #endregion

        #region methods ---------------------------------------------------------------
		//TODO test this code on online conditions
        private void OnTriggerStay(Collider other)
		{
			if (other.gameObject.tag.Equals("Player") && counter && canAppear)
            {
                anim.SetBool("onRange", true);
                counter = false;
            }
		}
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag.Equals("Player") && counter == false)
            {
                anim.SetBool("onRange", false);
                counter = true;
            }
        }
        #endregion
    }
}