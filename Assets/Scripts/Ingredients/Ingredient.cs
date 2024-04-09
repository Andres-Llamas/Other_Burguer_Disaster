using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonEnumerates;

namespace Ingredients
{
    public class Ingredient : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------	
        [SerializeField] private foodType _typeOfIngredient;
		public foodType TypeOfIngredient
		{
			get { return _typeOfIngredient; }			
		}
		
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        #endregion

        #region methods ---------------------------------------------------------------
        #endregion
    }
}