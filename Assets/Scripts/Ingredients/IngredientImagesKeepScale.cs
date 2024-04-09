using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientImagesKeepScale : MonoBehaviour
{
    [SerializeField] Vector3 scaleAtEnabled;

	private void OnEnable()
	{
        this.transform.localScale = scaleAtEnabled;
    }
}
