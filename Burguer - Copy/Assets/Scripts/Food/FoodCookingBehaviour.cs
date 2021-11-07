using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z_utilites.Enumerates;

namespace Food
{
    public class FoodCookingBehaviour : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        [SerializeField] private Renderer _renderer;
        public Color originalColor;
        public Color mediumCooked;
        public Color cookedColor;
        public Color mediumBurned;
        public Color burnedColor;
        public float cookTime;
        public float burnTime;
        public FoodCookedState cookedState = FoodCookedState.raw;
        public bool modifyStartColor;
        #endregion

        #region NO_Inspector variables ------------------------------------------------		        
        FoodParenting parenting;
        bool cooking;
        int cookedStateCounter = 0;
        public bool colorTest;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            parenting = GetComponent<FoodParenting>();
            if (_renderer == null)
                _renderer = GetComponentInChildren<MeshRenderer>();
        }

        private void Start()
        {
            if (modifyStartColor)
                ChangeColor(originalColor);
        }
        private void Update()
        {
            if (colorTest)
                ChangeColor(originalColor);
        }
        private void OnEnable()
        {
            parenting.onParented += StopCooking;
        }
        private void OnDisable()
        {
            parenting.onParented -= StopCooking;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("Grill"))
            {
                if (cooking == false)
                    StartCooking();
            }
        }

        public void StartCooking()
        {
            print("Cooking");
            StartCoroutine(nameof(ChangingColor));
            cooking = true;
        }
        public void StopCooking()
        {
            if (cooking == true)
            {
                print("SToop cooking");
                StopAllCoroutines();
                cooking = false;
            }
        }

        void ChangeColor(Color color)
        {
            _renderer.material.color = color;
        }

        IEnumerator ChangingColor()
        {
            yield return new WaitForSeconds(cookTime * 0.5f);// medium cooked			
            if (cookedStateCounter < 1)
            {
                cookedState = FoodCookedState.medium;
                cookTime = cookTime * 0.5f;
                ChangeColor(mediumCooked);
                cookedStateCounter++;
            }
            yield return new WaitForSeconds(cookTime);
            if (cookedStateCounter < 2)
            {
                cookedState = FoodCookedState.cooked;
                cookTime = 0;
                ChangeColor(cookedColor);
                cookedStateCounter++;
            }
            yield return new WaitForSeconds(burnTime * 0.5f);// medium burned
            if (cookedStateCounter < 3)
            {
                cookedState = FoodCookedState.mediumBurned;
                burnTime = burnTime * 0.5f;
                ChangeColor(mediumBurned);
                cookedStateCounter++;
            }
            yield return new WaitForSeconds(burnTime);
            if (cookedStateCounter < 4)
            {
                burnTime = 0;
                cookedState = FoodCookedState.burned;
                ChangeColor(burnedColor);
                cookedStateCounter++;
            }
        }
        #endregion
    }
}