using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Food;
using Z_utilites.Enumerates;

namespace Entities.NPC
{
    public class CompareFood : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        public List<FoodType> ingredientsCriteria;
        public float totalPoints;
        [Header("Criteria options")]
        public bool useAnyIngredient = false;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        public delegate void Notify();
        public event Notify onIngredientsChecked;
        public List<GameObject> ingredientsAlredyChecked;
        bool canCheckTrigger = true;
        Dialogue dialogue;
        #endregion

        #region methods ---------------------------------------------------------------	

        private void Awake()
        {
            ingredientsCriteria = new List<FoodType>();
            dialogue = GetComponent<Dialogue>();
        }

        private void Start()
        {
            GenerateFoodCriteria();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (canCheckTrigger)
            {
                if (other.gameObject.layer == 7)// 7 dinamic
                {
                    Transform otherRoot = other.transform.root;
                    if (otherRoot.gameObject.tag != "Player")
                    {
                        canCheckTrigger = false;
                        GetChildren(otherRoot);
                        print(totalPoints + " / " + (ingredientsCriteria.Count-1));
                        totalPoints = (totalPoints / ingredientsCriteria.Count-1) * 100;
                        onIngredientsChecked.Invoke();
                        GameObject.Destroy(otherRoot.gameObject);
                    }
                }
            }
        }

        void GetChildren(Transform trans)
        {
            foreach (Transform child in trans)
            {
                if (child.gameObject.layer == 7)
                {
                    Compare(child.GetComponent<FoodAtributes>());
                    Debug.Log(child.GetComponent<FoodAtributes>());
                    if (child.childCount > 0)
                    {
                        GetChildren(child);
                    }
                }
            }
        }

        void Compare(FoodAtributes food)
        {
            // Compare the ingredients in a food base with the requested ingredients from the NPC            
            if (ingredientsAlredyChecked.Contains(food.gameObject) == false)
            {
                foreach (FoodType type in ingredientsCriteria)
                {
                    if (food.TypeOfFood == type)
                    {
                        totalPoints++;
                        ingredientsAlredyChecked.Add(food.gameObject);
                        break;
                    }
                }
            }
        }
        [NaughtyAttributes.Button]
        void GenerateFoodCriteria()
        {
            ingredientsCriteria.Clear();
            ingredientsCriteria.Add(FoodType.lowerBread);
            dialogue.InstantiateImageOfFood(1); // to instantiate upper bread
            int numberOfIngredients = Random.Range(0, 10);
            for (int i = 0; i < numberOfIngredients; i++)
            {
                int randomIngredient = 0;
                if (useAnyIngredient)
                {
                    randomIngredient = Random.Range(2, FoodType.GetNames(typeof(FoodType)).Length); // start at 2 to not include breads                    
                }
                else
                {
                    randomIngredient = Random.Range(2, FoodType.GetNames(typeof(FoodTypeOnlyNormalFood)).Length);
                }
                while (randomIngredient == 8 || randomIngredient == 9)// to disable temporary fries and packet
                {
                    randomIngredient = Random.Range(2, FoodType.GetNames(typeof(FoodType)).Length);
                }
                ingredientsCriteria.Add((FoodType)randomIngredient);
                dialogue.InstantiateImageOfFood(randomIngredient);
            }
            ingredientsCriteria.Add(FoodType.upperBread);
            dialogue.InstantiateImageOfFood(0); // to instantiate lower bread            
        }

        [NaughtyAttributes.Button]
        /// <summary>
        /// To reset the NPC variables to use it again
        /// </summary>
        public void Reset()
        {
            ingredientsAlredyChecked.Clear(); ;
            canCheckTrigger = true;
            totalPoints = 0;
        }
        #endregion
    }
}