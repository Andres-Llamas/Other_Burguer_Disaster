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
        //Not in use anymore - public bool canStartAttaching; // this is in order to avoid breads attach each other if are pillded just once
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        public bool isTopOnFood = false; // this indicates if the hamburhuer is done
        /// <summary>
        /// Deprecated.
        /// isThisTheMajorBase was original in order to avoid errors if there was more lower breads on the
        /// hamburguer, but thanks to no desired attachments among the breads when first appearing pilled ,
        /// I decided to just not allow more lower breads to be attached to a lower bread
        /// 
        /// I wont delete this because I am shure I will broke something
        /// </summary>
        public bool isThisTheMajorBase; // in case there is more bases attached to this, this indicate this is the bottom base

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
            // to check for top bread wich indicates the burguer is done and stop attaching more food tho this
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