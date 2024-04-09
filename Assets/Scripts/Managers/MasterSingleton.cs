using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Singletons
{
    public class MasterSingleton : MonoBehaviour
    {
        #region Public ---------------------------------------------------
        public static MasterSingleton Instance { get; private set; }
        public InputManager InputManager { get; private set; }
        public FoodImageManager foodImageManager { get; private set; }
        #endregion

        #region methods ---------------------------------------------------------------

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            InputManager = GetComponentInChildren<InputManager>();
            foodImageManager = GetComponentInChildren<FoodImageManager>();
        }
        #endregion
    }
}