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
            InvokeRepeating(nameof(Updating), 0, 0.1f);
        }

        private void Updating()
        {
            if (this.transform.parent == null)
            {
                // if this object is not as achild of other object or in the hand of the player, then 
                // activate physics
                thisObjectAtributtes.Rigid_body.isKinematic = false;
                thisObjectAtributtes.SolidCollider.enabled = true;
            }
            else
            {
                // this is in order to avoid collision problems when this object is on the hand of the 
                // player or as achild of a food
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
            if (other.gameObject.layer == 7) // if other is a dinamic object
            {
                if (this.transform.localPosition.y > other.transform.localPosition.y) // if this object is avobe the other
                {
                    if (other.transform.root.tag == "Food base" && thisObjectOnHand == false && !this.gameObject.tag.Equals("Food base"))
                    {                        
                        if (foodBase == null)
                        {
                            foodBase = other.transform.root.GetComponent<FoodBase>(); // get the base bread
                            if (foodBase.isTopOnFood == false) // this is to avoid adding more ingredients once the hambutger is finish
                            {
                                this.transform.parent = other.transform;
                                foodBase.AddObjectToIngredients(thisObjectAtributtes);
                                ReacomodateThisObject(other);
                            }
                            else // if the other object has alredy a top bread (wich means the hamburguer is finished), then forget the other object in order to recieve another
                                foodBase = null;
                        }
                    }
                }
            }
        }

        void ReacomodateThisObject(Collider other)
        {
            // this is to move this object avobe the other a little in order to avoid transposition of objects.. but it does not work too well... 
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
                            // if this object has children, then for each child call the foodBase reference that
                            // each child has in foodAtributes and call the function Remove 
                            foodBase.RemoveObjectFromIngredients(child.GetComponent<FoodAtributes>());
                            child.GetComponent<FoodParenting>().foodBase = null;// to being able the other objects to start parenting again

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