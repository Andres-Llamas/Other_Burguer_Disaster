using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_DeathZone : MonoBehaviour
{
    #region Inspector variables ---------------------------------------------------	
    #endregion

    #region NO_Inspector variables ------------------------------------------------
    #endregion

    #region methods ---------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("NPC"))
        {
            Destroy(other.gameObject);
        }
    }
    #endregion
}
