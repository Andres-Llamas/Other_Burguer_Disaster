using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SO.Variables
{
    [CreateAssetMenu(fileName = "Bool variable", menuName = "Variables/Bool Variable")]
    public class SO_BoolVariable : ScriptableObject
    {
        #region Inspector variables ---------------------------------------------------	
#if UNITY_EDITOR
        [ResizableTextArea]
        public string Description;
#endif
        public bool value;
		[Tooltip("The value to be reseted when start the game or enter play mode again")]
        public bool originalValue;// the value the variable will have when entering play mode, is kind a reset 
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        #endregion

        #region methods ---------------------------------------------------------------
        private void OnEnable()
        {
            ResetCurrentValue();
        }

        public void ResetCurrentValue()
        {
            value = originalValue;
        }
        /// <summary>
        /// Change the value of the bool variable
        /// </summary> 
        /// <param name="value"></param>
        public void ChangeCurrentValue(bool value)
        {
            this.value = value;
        }
        #endregion
    }
}