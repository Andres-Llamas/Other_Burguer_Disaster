using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------	        
        [Header("Options")]
        public float speed = 1000;
        public float maxVelocity;
        public float friciton = 1f;
        [Header("Other")]
        public LayerMask ground;
        public Transform footPosition;
        [ReadOnly]
        public bool grounded;
        [ReadOnly]
        public bool moving;
        public float steepSmooth;
        public Rigidbody rb;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        PlayerInputsManager inputs;
        Vector2 inputDirection;
        Vector3 velocity;
        float copyFriction;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            inputs = GetComponent<PlayerInputsManager>();
            //rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            copyFriction = friciton;
        }

        private void FixedUpdate()
        {
            Movement();
        }
        private void Update()
        {
            Friction();
            MaxVelocity();
        }
        private void OnEnable()
        {
            inputs.movement.performed += ctx => moving = true;
            inputs.movement.canceled += ctx => moving = false;
        }
        void Movement()
        {
            grounded = Physics.CheckSphere(footPosition.position, 0.4f, ground);
            inputDirection = inputs.movement.ReadValue<Vector2>();
            Vector3 direction = this.transform.right * inputDirection.x + this.transform.forward * inputDirection.y;
            rb.AddForce(direction * speed * Time.deltaTime);
        }
        void MaxVelocity()
        {
            if (grounded)
            {
                if (rb.velocity.magnitude > maxVelocity)
                {
                    rb.velocity = rb.velocity.normalized * maxVelocity;
                }
            }
        }
        void Friction()
        {
            if (grounded)
            {
                if (moving == false)
                {
                    rb.velocity *= Mathf.Lerp(friciton, 0, Time.deltaTime);
                }
                else
                {
                    friciton = copyFriction;
                }
            }
            #endregion
        }
    }
}