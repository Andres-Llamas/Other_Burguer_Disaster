using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputsManager : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------	
        public InputActionAsset inputs;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        [HideInInspector] public InputAction movement;
        [HideInInspector] public InputAction look;
        [HideInInspector] public InputAction rightArm;
        [HideInInspector] public InputAction lefttArm;
        [HideInInspector]public InputAction rotate;
        [HideInInspector]public InputAction scroll;
        [HideInInspector] public InputAction escape;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            movement = inputs.FindActionMap("Gameplay").FindAction("Move");
            look = inputs.FindActionMap("Gameplay").FindAction("Look");
            rightArm = inputs.FindActionMap("Gameplay").FindAction("Right arm");
            lefttArm = inputs.FindActionMap("Gameplay").FindAction("Left arm");
            rotate = inputs.FindActionMap("Gameplay").FindAction("Rotate");
            scroll = inputs.FindActionMap("Gameplay").FindAction("Scroll");
            escape = inputs.FindActionMap("Gameplay").FindAction("Escape");
            inputs.Enable();
        }        
        #endregion
    }
}