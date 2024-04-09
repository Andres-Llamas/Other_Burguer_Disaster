using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Ingredients
{
    public class IngredientBaseStaking : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        [SerializeField][Tag] string ingredientTag;
        [SerializeField][Tag] string topIngredientTag;
        [ReadOnly] public List<Transform> ingredientsInThisObject = new List<Transform>();
        [SerializeField] int childCount;
        [SerializeField] float moveUpwardUnits=1;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        Transform[] children;
        BoxCollider _collider;
        #endregion

        #region methods ---------------------------------------------------------------        
        private void Start()
        {
            _collider = GetComponent<BoxCollider>();
            InvokeRepeating(nameof(GetThisObjectChildren), 0, 0.15f);
            InvokeRepeating(nameof(CheckForNewIngredients), 0, 0.2f);
            InvokeRepeating(nameof(CheckForAbsentIngredients), 0, 0.3f);
        }

        private void Update()
        {
            childCount = this.transform.childCount;
        }

        //TODO Make these methods being called by events instead of constant updates
        void CheckForNewIngredients()
        {
            if (children.Length > 0)
            {
                foreach (Transform child in children)
                {
                    if (ingredientsInThisObject.Contains(child) == false)
                    {
                        if (child.gameObject.tag.Equals(ingredientTag))
                        {
                            ingredientsInThisObject.Add(child);
                        }
                        else if (child.gameObject.tag.Equals(topIngredientTag))
                        {
                            SetIngredientsAsAOnlyObject(children, child);
                            CancelInvoke(nameof(CheckForNewIngredients));
                        }
                    }
                }
            }
        }

        void CheckForAbsentIngredients()
        {
            if (children.Length > 0)
            {
                if (ingredientsInThisObject.Count > 0)
                {
                    bool wasIngredientFoundAsChild;
                    foreach (Transform ingredient in ingredientsInThisObject.ToArray())
                    {
                        wasIngredientFoundAsChild = false;
                        foreach (Transform child in children)
                        {
                            if (ingredient == child)
                            {
                                wasIngredientFoundAsChild = true;
                            }
                        }
                        if (wasIngredientFoundAsChild == false)
                        {
                            ingredientsInThisObject.Remove(ingredient);
                        }
                    }
                }
            }
        }

        void GetThisObjectChildren()
        {
            children = this.transform.GetComponentsInChildren<Transform>();
        }

        void SetIngredientsAsAOnlyObject(Transform[] childrenInThisObject, Transform topIngredient)
        {
            // this script is intended to act once the top bread of the hamburger is placed on this as a child
            // once that happens, it means the burger is ready and it will block the player to grab any ingredient 
            // from this object separately, instead, it will grab all the ingredients as a whole (like grabbing a complete burger)
            foreach (Transform child in childrenInThisObject)
            {
                if (child.gameObject != this.gameObject)
                {
                    if (child.gameObject.tag.Equals(ingredientTag) || child.gameObject.tag.Equals(topIngredientTag))
                    {
                        Collider ingredientCollider = child.GetComponent<Collider>();
                        ingredientCollider.enabled = false;
                    }
                }
            }
            float newSizeOfColliderInY = topIngredient.transform.position.y - this.transform.position.y;
            _collider.center = new Vector3(_collider.center.x, (newSizeOfColliderInY / 2) + _collider.center.y, _collider.center.z);
            _collider.size = new Vector3(_collider.size.x, newSizeOfColliderInY, _collider.size.z);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + moveUpwardUnits, this.transform.position.z);// to move this object a little upward in order to avoid transposing the ground
        }
        #endregion
    }
}