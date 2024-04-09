using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO.Variables
{
    [CreateAssetMenu(fileName = ("String Variable"), menuName = ("Variables/String Variable"))]
    public class StringVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string Description;
#endif
        public string currentText;
        public string originalText;


        private void OnEnable()
        {
            ResetCurrentValue();
        }

        public void ResetCurrentValue()
        {
            currentText = originalText;
        }
        public void ReplaceCurrentText(string text)
        {
            currentText = text;
        }
    }
}