using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CangeCrosshairColor : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------		
        public Player.PlayerArmsDetection playerDetection;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        Image crosshair;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Start()
        {
            crosshair = GetComponent<Image>();
        }
		private void Update()
		{
			ChangeColor();
		}
        void ChangeColor()
        {
            if (playerDetection.objectDetected)
                crosshair.color = Color.green;
            else
                crosshair.color = Color.white;
        }
        #endregion
    }
}