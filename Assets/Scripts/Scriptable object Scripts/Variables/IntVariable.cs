using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO.Variables
{
    [CreateAssetMenu(fileName = ("Int Variable"), menuName = ("Variables/Int Variable"))]
    public class IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string Description;
#endif
        public int currentValue;
        public int originalValue;

        private void OnEnable()
        {
            ResetCurrentValue();
        }

        public void ResetCurrentValue()
        {
            currentValue = originalValue;

        }

        /// <summary>
        /// Increase or decrease the currentValue of the object
        /// <para>If you want to substract use the sign "-" in the parameter.</para> 
        /// </summary> 
        /// <param name="ammount"></param>
        public void ChangeCurrentValue(int ammount)
        {
            currentValue += ammount;
        }
    }
}