using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonEnumerates;

namespace SO.Variables
{
    public class StringReference
    {
        public VarType varType;
        public StringVariable globalText;
        public string literalText;
        public string text
        {
            get
            {
                if (varType == VarType.global)
                {
                    return globalText.currentText;
                }
                return literalText;
            }
        }

        /// <summary>
        /// Increase or decrease the currentValue of the object
        /// <para>If you want to substract use the sign "-" in the parameter.</para> 
        /// </summary> 
        /// <param name="ammount"></param>
    }
}