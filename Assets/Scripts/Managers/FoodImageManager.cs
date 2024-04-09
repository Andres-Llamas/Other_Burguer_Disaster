using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonEnumerates;

namespace Managers
{
    public class FoodImageManager : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        [SerializeField] GameObject lowerBread;
        [SerializeField] GameObject upperBread;
        [SerializeField] GameObject onionSlice;
        [SerializeField] GameObject tomatoSlice;
        [SerializeField] GameObject lettuceSlice;
        [SerializeField] GameObject burgerPatty;
        [SerializeField] GameObject beaconSlice;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        #endregion

        #region methods ---------------------------------------------------------------
        /// <summary>
        /// To instantiate gameObjects with the image of the ingredients needed
        /// </summary>
        /// <param name="referenceTransform">start position to begin instantiating</param>
        /// <param name="listOfIngredients"></param>
        /// <param name="downwardOffset">the distance the images will separate each other downward</param>
        public void InstantiateImages(Transform referenceTransform, List<foodType> listOfIngredients, float downwardOffset, float scaleFactor)
        {
            Vector3 currentInstantiatingPosition = referenceTransform.position;
            GameObject ingredientInstance = null;

            foreach (foodType ingredient in listOfIngredients)
            {
                switch (ingredient)
                {                    
                    case foodType.onionSlice:
                        ingredientInstance = Instantiate(onionSlice, currentInstantiatingPosition, referenceTransform.rotation, referenceTransform);
                        break;
                    case foodType.tomatoSlice:
                        ingredientInstance = Instantiate(tomatoSlice, currentInstantiatingPosition, referenceTransform.rotation, referenceTransform);
                        break;
                    case foodType.lettuceSlice:
                        ingredientInstance = Instantiate(lettuceSlice, currentInstantiatingPosition, referenceTransform.rotation, referenceTransform);
                        break;
                    case foodType.patty:
                        ingredientInstance = Instantiate(burgerPatty, currentInstantiatingPosition, referenceTransform.rotation, referenceTransform);
                        break;
                    case foodType.beacon:
                        ingredientInstance = Instantiate(beaconSlice, currentInstantiatingPosition, referenceTransform.rotation, referenceTransform);
                        break;
                    default:                        
                        break;
                }
                ingredientInstance.transform.localScale = new Vector3(ingredientInstance.transform.localScale.x - scaleFactor,
                                                                    ingredientInstance.transform.localScale.y - scaleFactor,
                                                                    ingredientInstance.transform.localScale.z - scaleFactor);
                currentInstantiatingPosition = new Vector3(currentInstantiatingPosition.x,
                                                            currentInstantiatingPosition.y - downwardOffset,
                                                            currentInstantiatingPosition.z);
            }
        }
        #endregion
    }
}