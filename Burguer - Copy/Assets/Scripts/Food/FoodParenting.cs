using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodParenting : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        public delegate void Notify();
        public event Notify onParented;
        FoodAtributes thisObjectAtributtes;
        FoodBase foodBase;
        bool thisObjectOnHand;
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
            ParentThisObjectToOther(other);
        }

        void ParentThisObjectToOther(Collider other)
        {
            if (other.gameObject.layer == 7)
            {
                if (this.transform.localPosition.y > other.transform.localPosition.y)
                {
                    if (other.transform.root.tag == "Food base" && thisObjectOnHand == false)
                    {
                        if (foodBase == null)
                        {
                            foodBase = other.transform.root.GetComponent<FoodBase>();
                            if (foodBase.isTopOnFood == false)
                            {
                                this.transform.parent = other.transform;
                                foodBase.AddObjectToIngredients(thisObjectAtributtes);
                                ReacomodateThisObject(other);
                            }
                            else
                                foodBase = null;
                        }
                    }
                }
            }
        }

        void ReacomodateThisObject(Collider other)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                  this.transform.localPosition.y + other.GetComponent<FoodAtributes>().ColliderSize,
                                                  this.transform.localPosition.z);
        }

        /// <summary>
        /// To quit this food object from the food base ingredient list, and if this 
        /// object has children, then quit from the food base ingredient list all the childrens
        /// </summary>
        public void QuitObjectFromIngredientList()
        {
            if (foodBase != null)
            {
                foodBase.RemoveObjectFromIngredients(thisObjectAtributtes);
                if (this.transform.childCount > 0)
                {
                    foreach (Transform child in this.transform)
                    {
                        if (child.gameObject.layer == 7)// 7 = dinamic
                        {
                            foodBase.RemoveObjectFromIngredients(child.GetComponent<FoodAtributes>());
                            child.GetComponent<FoodParenting>().foodBase = null;

                        }
                    }
                }
                foodBase = null;
            }
        }

        public void RiseOnParentedEvent()
        {
            if (onParented != null)
                onParented.Invoke();
            thisObjectOnHand = true;
        }

        public void RiseOnUnparentedEvent()
        {
            thisObjectOnHand = false;
        }
        #endregion
    }
}