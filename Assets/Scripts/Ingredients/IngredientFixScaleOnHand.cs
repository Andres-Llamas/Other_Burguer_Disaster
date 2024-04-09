using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientFixScaleOnHand : MonoBehaviour
{
    #region Inspector variables ---------------------------------------------------
    public bool scaleInHand = true;
    [SerializeField] Vector3 scaleToBeSet = Vector3.one;
    #endregion

    #region NO_Inspector variables ------------------------------------------------	
    #endregion

    #region methods ---------------------------------------------------------------
    private void Update()
    {
        if (this.transform.root.gameObject.tag.Equals("Player"))
        {
            if (scaleInHand)
            {
                this.transform.localScale = SetGlobalScale(this.transform, scaleToBeSet);
            }
        }
    }

    Vector3 SetGlobalScale(Transform _transform, Vector3 globalScale)
    {
        _transform.localScale = Vector3.one;
        _transform.localScale = new Vector3(globalScale.x / _transform.lossyScale.x, globalScale.y / _transform.lossyScale.y, globalScale.z / _transform.lossyScale.z);
        return _transform.localScale;
    }
    #endregion
}
