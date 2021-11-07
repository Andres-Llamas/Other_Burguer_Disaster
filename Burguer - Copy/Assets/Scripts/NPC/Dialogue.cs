using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO.List;

namespace Entities.NPC
{
    public class Dialogue : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------		
        public GameObject foodImagesPlaceHolder, upperBound, middleBound;
        public SO_List_GameObject ingredietsGlobalList; // The list of all ingredients from wich we will reference for the images
        #endregion

        #region NO_Inspector variables ------------------------------------------------	
        float imagesOffsetCounterY;
		float hamburgerOffsetCounterY;
        float imagesOffsetCounterX;
        int rowCounter = 1; // the maximum number of ingredients in one row
        int renderPriority = 3000; // the priority for the ingredients images to make a hamburguer image        
        #endregion

        #region methods ---------------------------------------------------------------

        public void InstantiateImageOfFood(int foodIndex)
        {
            var ingredient = Instantiate(ingredietsGlobalList.Items[foodIndex], foodImagesPlaceHolder.transform);
            CreateHamburguerImage(foodIndex);
            if (rowCounter > 4)
            {
                imagesOffsetCounterX += 0.25f;
                imagesOffsetCounterY = 0;
                rowCounter = 0;
            }
            ingredient.transform.localPosition = new Vector3(0, upperBound.transform.localPosition.y - imagesOffsetCounterY, imagesOffsetCounterX);
            imagesOffsetCounterY += 0.2f;
            rowCounter++;
        }

        void CreateHamburguerImage(int foodIndex)
        {
            // To instantiate image at image of ingredients to get at the end a formed hamburguer image
            var ingredient = Instantiate(ingredietsGlobalList.Items[foodIndex], foodImagesPlaceHolder.transform);            
			ingredient.transform.localPosition = new Vector3(0, middleBound.transform.localPosition.y-hamburgerOffsetCounterY, middleBound.transform.localPosition.z);
            Renderer _renderer = ingredient.GetComponentInChildren<MeshRenderer>();
            _renderer.material.renderQueue = renderPriority;
            renderPriority--;
			hamburgerOffsetCounterY +=0.05f;
        }
        #endregion
    }
}