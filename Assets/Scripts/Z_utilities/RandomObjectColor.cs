using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z_utilities.Misc
{
    public class RandomObjectColor : MonoBehaviour
    {
        private void Awake()
        {
            this.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0.0f, 1.0f, 0.75f, 1.0f, 0.5f, 1.0f);
        }
    }
}