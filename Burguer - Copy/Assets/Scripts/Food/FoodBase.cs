using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    public class FoodBase : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        [SerializeField] private List<FoodAtributes> ingredients;
        public List<FoodAtributes> Ingredients
        {
            get { return ingredients; }
        }
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        public bool isTopOnFood = false;
        public bool isThisTheMajorBase;
        #endregion

        #region methods ---------------------------------------------------------------
        public void AddObjectToIngredients(FoodAtributes other)
        {
            if (ingredients.Contains(other) == false)
                ingredients.Add(other);
            isTopOnFood = CheckForFoodTop();
            CheckForMoreBases();
        }

        public void RemoveObjectFromIngredients(FoodAtributes other)
        {
            if (ingredients.Contains(other))
                ingredients.Remove(other);
            isTopOnFood = CheckForFoodTop();
            CheckForMoreBases();
        }

        bool CheckForFoodTop()
        {
            foreach (FoodAtributes ingredient in ingredients)
            {
                if (ingredient.gameObject.tag.Equals("Food top"))
                {
                    return true;
                }
            }
            return false;
        }

        void CheckForMoreBases()
        {
            foreach (FoodAtributes ingredient in this.transform.root.GetComponent<FoodBase>().ingredients)
            {
                if (ingredient.gameObject.tag.Equals("Food base"))
                {
                    if (ingredient.transform == ingredient.transform.root)
                    {
                        ingredient.GetComponent<FoodBase>().isThisTheMajorBase = true;
                    }
                    else
                    {
                        ingredient.GetComponent<FoodBase>().isThisTheMajorBase = false;
                    }
                }
            }
        }
        #endregion
    }
}