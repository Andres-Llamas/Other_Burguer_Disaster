using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------	
        [Header("Options")]
        public float sensitivity = 100;
        [Header("Other")]
        public PlayerInputsManager inputs;
        public Transform body;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        Vector2 mousePosition;
        float verticalLook = 0;
        bool rotateButtonPressed;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Start()
        {
            HidePointer();
        }

        private void OnEnable()
        {
            StartCoroutine(nameof(Initialize));
        }
        IEnumerator Initialize()
        {
            // I dont know why OnEnable code wont work for this inputs
            yield return new WaitForEndOfFrame();
            inputs.rotate.started += ctx => rotateButtonPressed = true;
            inputs.rotate.canceled += ctx => rotateButtonPressed = false;
            inputs.escape.performed += ctx => ShowPointer();
        }
        private void OnDisable()
        {
            inputs.rotate.started += ctx => rotateButtonPressed = true;
            inputs.rotate.canceled += ctx => rotateButtonPressed = false;
        }

        private void Update()
        {
            LookMovement();
        }

        void LookMovement()
        {
            if (rotateButtonPressed == false)
            {
                mousePosition = inputs.look.ReadValue<Vector2>() * sensitivity * Time.deltaTime;
                // vertical camera movement, x axis rotation
                verticalLook -= mousePosition.y;
                verticalLook = Mathf.Clamp(verticalLook, -90, 90);
                this.transform.localRotation = Quaternion.Euler(verticalLook, 0, 0);
                // Horizontal camera movement, y axis rotation            
                body.Rotate(new Vector3(0, mousePosition.x));
            }
        }

        void ShowPointer()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        void HidePointer()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        #endregion
    }
}