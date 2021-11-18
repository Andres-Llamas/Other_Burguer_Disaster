using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z_utilites.Enumerates;

namespace Food
{
    /// <summary>
    /// A Script that works just for reference
    /// </summary>
    public class FoodAtributes : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        [SerializeField] FoodType typeOfFood;
        public FoodType TypeOfFood
        {
            get { return typeOfFood; }
        }
        [SerializeField] private float _colliderSize = 0.001f; // the size that will be use to reacomodate the object
        public float ColliderSize
        {
            get { return _colliderSize; }
        }
        [SerializeField] private float _objectSizeOnHand = 20;
        public float ObjectSizeOnHand
        {
            get { return _objectSizeOnHand; }
        }

        [SerializeField] private Collider _solidCollider;
        public Collider SolidCollider
        {
            get { return _solidCollider; }
        }

        [SerializeField] private Collider _triggerCollider;
        public Collider TriggerCollider
        {
            get { return _triggerCollider; }
        }

        [SerializeField] private Rigidbody _rigidbody;
        public Rigidbody Rigid_body
        {
            get { return _rigidbody; }
        }

        #endregion

        #region NO_Inspector variables ------------------------------------------------
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            if (_solidCollider == null)
                _solidCollider = GetComponentInChildren<MeshCollider>();
            if (_triggerCollider == null)
                _triggerCollider = GetComponent<BoxCollider>();
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
        }
        #endregion
    }
}