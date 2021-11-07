using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodPlateParenting : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        FoodAtributes thisObjectAtributtes;

        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            thisObjectAtributtes = GetComponent<FoodAtributes>();
        }

        private void Update()
        {
            if (this.transform.parent == null)
            {
                thisObjectAtributtes.Rigid_body.isKinematic = false;
                thisObjectAtributtes.SolidCollider.enabled = true;
            }
            else
            {
                thisObjectAtributtes.SolidCollider.enabled = false;
                thisObjectAtributtes.Rigid_body.isKinematic = true;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.gameObject.layer == 7 
                && other.transform.parent == null
                && this.transform.parent == null
                && other.transform.position.y > this.transform.position.y)
            {
                if (CheckIfThisHasAHamburguer() == false)
                {
                    other.transform.position = this.transform.position;
                    other.transform.parent = this.transform;
                    other.transform.position = new Vector3(other.transform.position.x,
                                                                other.transform.position.y + thisObjectAtributtes.ColliderSize,
                                                                other.transform.position.z);
                }
            }
        }

        bool CheckIfThisHasAHamburguer()
        {
            if (this.transform.childCount > 1)
                return true;
            else
                return false;
        }
        #endregion
    }
}