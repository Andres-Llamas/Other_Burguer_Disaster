using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_utilites
{

    public class LookAtCamera : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        public float flipY;
        public float flipX;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        #endregion

        #region methods ---------------------------------------------------------------
        private void LateUpdate()
        {
            if (GetComponent<MeshRenderer>().isVisible)
            {
                this.transform.LookAt(Camera.main.transform);
                this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y + flipY, this.transform.rotation.eulerAngles.z);
            }
        }
        #endregion
    }
}