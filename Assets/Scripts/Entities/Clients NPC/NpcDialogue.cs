using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singletons;
using System;
using NaughtyAttributes;

namespace Entities.Client
{
    public class NpcDialogue : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        [Header("Initialize variables")]
        [SerializeField] GameObject DialogueBubble;
        [SerializeField] Transform imageInstantiationStartingPosition;
        [Header("Options")]
        [SerializeField] GameObject upperBreadImage;
        [SerializeField] GameObject bottomBreadImage;
        [SerializeField] float rate = 0.01f;  // Rate of change 
        [ReadOnly][SerializeField] float offsetPosition = 0.1f;
        [SerializeField] float ingredientsScaleMod;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        NpcFoodBehavior ingredientChecker;
        ClientPlayerDetector playerDetector;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            ingredientChecker = GetComponent<NpcFoodBehavior>();
            playerDetector = GetComponentInChildren<ClientPlayerDetector>();
        }

        private void OnEnable()
        {
            playerDetector.OnPlayerDetected += EnableDialogueBubble;
            playerDetector.OnPlayerStoppedBeingDetected += DisableDialogueBubble;
        }

        private void OnDisable()
        {
            playerDetector.OnPlayerDetected -= EnableDialogueBubble;
            playerDetector.OnPlayerStoppedBeingDetected -= DisableDialogueBubble;
        }

        private void Start()
        {            
            CalculateIngredientsOffset();
            MasterSingleton.Instance.foodImageManager.InstantiateImages(imageInstantiationStartingPosition, ingredientChecker.clientIngredientsOrder, offsetPosition, ingredientsScaleMod);
        }

        void EnableDialogueBubble()
        {
            DialogueBubble.SetActive(true); //TODO replace for an appear animation
        }

        void DisableDialogueBubble()
        {
            DialogueBubble.SetActive(false);//TODO replace for an disappear animation
        }

        [Button]
        void CalculateIngredientsOffset()
        {
            offsetPosition = DecreaseInverselyProportional();
        }

        float DecreaseInverselyProportional()
        {
            float breadDistance = Vector3.Distance(upperBreadImage.transform.position, bottomBreadImage.transform.position);
            float offset = breadDistance / ingredientChecker.clientIngredientsOrder.Count;
            return offset - rate;
        }
        #endregion
    }
}