using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonEnumerates;
using Ingredients;
using NaughtyAttributes;

namespace Entities.Client
{
    public class NpcFoodBehavior : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------	
        [Header("Configuration")]
        [SerializeField] LayerMask _ingredietsLayer;
        [SerializeField][Tag] string _ingredientTag;
        [SerializeField] Vector3 _boxCastHalfExtens = new Vector3(1.93f, 0.17f, 1.22f);
        [SerializeField] float _detectionMaxDistance = 1;
        [SerializeField] Transform _detectionCenter;
        [Header("Food detection options")]
        public List<foodType> clientIngredientsOrder;
        [SerializeField][ReadOnly] private int _totalAccuracyScore;
        [ReadOnly] public bool clientHasBeenAttended = false; // if the player has given any food to this npc
        public int TotalAccuracyScore
        {
            get { return _totalAccuracyScore; }
        }

        #endregion

        #region NO_Inspector variables ------------------------------------------------
        ClientFoodDetector foodDetector;
        [ReadOnly]public List<foodType> ingredientsRemainingToCheck;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            foodDetector = GetComponentInChildren<ClientFoodDetector>();
        }

        private void OnEnable()
        {            
            ResetNPCBehavior();
            foodDetector.foodDetected += CheckIngredients;
        }

        private void OnDisable()
        {
            ResetNPCBehavior();
            foodDetector.foodDetected += CheckIngredients;
        }

        void CheckIngredients(GameObject ingredientDetected)
        {
            Transform _elementDetectedObject = ingredientDetected.transform.root;            

            if (_elementDetectedObject.parent == null)
            {
                clientHasBeenAttended = true;
                foreach (Transform element in _elementDetectedObject.GetComponentsInChildren<Transform>())
                {
                    if (element.tag.Equals(_ingredientTag))
                    {
                        Ingredient currentIngredient = element.GetComponent<Ingredient>();
                        bool wasThisIngredientCorrect = false;
                        foreach (foodType type in ingredientsRemainingToCheck)
                        {
                            if (currentIngredient.TypeOfIngredient == type)
                            {
                                _totalAccuracyScore++;
                                wasThisIngredientCorrect = true;
                                ingredientsRemainingToCheck.Remove(type);
                                break;
                            }
                        }
                        if(wasThisIngredientCorrect == false)
                            _totalAccuracyScore--;
                    }
                }
                Destroy(_elementDetectedObject.gameObject);
            }
        }

        /// <summary>
        /// For testing 
        /// </summary>
        [Button]
        void ResetNPCBehavior()
        {
            // TODO Randomize the ingredients list
            _totalAccuracyScore = 0;
            clientHasBeenAttended = false;
            ingredientsRemainingToCheck = new List<foodType>(clientIngredientsOrder);
        }
        #endregion
    }
}