using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGizmos : MonoBehaviour
{
    #region Inspector variables ---------------------------------------------------	
    public Transform position;
    public float radious;
    public Color color;
    #endregion

    #region NO_Inspector variables ------------------------------------------------
    #endregion

    #region methods ---------------------------------------------------------------
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(position.position, radious);
    }
    #endregion
}
