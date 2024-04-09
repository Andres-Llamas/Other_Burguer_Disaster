using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        [SerializeField] InputActionAsset _inputs;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        public delegate void notify();
        //Movement
        public InputAction MoveAction { get; private set; }
        public Vector2 MovementInputDirection { get; private set; }
        public bool IsPlayerMoving { get; private set; }

        //Camera
        public InputAction LookAction { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool IsMovingCamera { get; private set; }

        //Scroll (wheel or gamepad d-pad)
        public InputAction ScrollAction { get; private set; }
        public Vector2 Scroll { get; private set; }
        public bool IsScrolling { get; private set; }

        //Zooming
        public InputAction ZoomingAction { get; private set; }
        public bool IsZoomingCamera { get; private set; }

        //running
        public InputAction SprintAction { get; private set; }
        public bool IsPlayerSprinting { get; private set; }

        //crouching
        public InputAction CrouchAction { get; private set; }
        public bool IsPlayerCrouching { get; private set; }

        //jumping
        public InputAction JumpAction { get; private set; }

        //Arms events
        public InputAction LeftArmAction { get; private set; }
        public notify leftArmActions;
        public bool IsLeftArmPressed { get; private set; }

        public InputAction RightArmAction { get; private set; }
        public notify rightArmActions;
        public bool IsRightArmPressed { get; private set; }

        //Rotate object on Hands
        public InputAction RotateObjectKeywordAction { get; private set; }
        public bool IsRotateObjectKeyboardPressed { get; private set; }

        public InputAction RotateLeftObjectControllerAction { get; private set; }
        public bool IsRotateLeftObjectControllerPressed { get; private set; }

        public InputAction RotateRightObjectControllerAction { get; set; }
        public bool IsRotateRightObjectControllerPressed { get; private set; }

        InputActionMap player;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            player = _inputs.FindActionMap("Player");

            MoveAction = player.FindAction("Move");
            LookAction = player.FindAction("Look");
            ScrollAction = player.FindAction("Scroll");
            ZoomingAction = player.FindAction("Zoom");
            SprintAction = player.FindAction("Sprint");
            JumpAction = player.FindAction("Jump");
            CrouchAction = player.FindAction("Sit");
            RotateObjectKeywordAction = player.FindAction("Rotate object (keyboard)");
            RotateLeftObjectControllerAction = player.FindAction("Rotate Left object (controller)");
            RotateRightObjectControllerAction = player.FindAction("Rotate Right object (controller)");
            LeftArmAction = player.FindAction("Left arm");
            RightArmAction = player.FindAction("Right arm");
        }

        private void OnEnable()
        {
            player.Enable();

            MoveAction.performed += SetMovement;
            MoveAction.canceled += SetMovement;

            LookAction.performed += SetLook;
            LookAction.canceled += SetLook;

            ScrollAction.performed += SetScroll;
            ScrollAction.canceled += SetScroll;

            ZoomingAction.started += SetZoom;
            ZoomingAction.canceled += SetZoom;

            SprintAction.started += SetRun;
            SprintAction.canceled += SetRun;

            CrouchAction.started += SetCrouch;
            CrouchAction.canceled += SetCrouch;

            RotateObjectKeywordAction.started += SetRotateObjectKeyboard;
            RotateObjectKeywordAction.canceled += SetRotateObjectKeyboard;

            RotateLeftObjectControllerAction.started += SetRotateObjectLeftHand;
            RotateLeftObjectControllerAction.canceled += SetRotateObjectLeftHand;

            RotateRightObjectControllerAction.started += SetRotateObjectRightHand;
            RotateRightObjectControllerAction.canceled += SetRotateObjectRightHand;

            LeftArmAction.started += InvokeLeftArmActions;
            LeftArmAction.started += SetLeftArmActions;
            LeftArmAction.canceled += SetLeftArmActions;

            RightArmAction.started += InvokeRightArmActions;
            RightArmAction.started += SetRightArmActions;
            RightArmAction.canceled += SetRightArmActions;
        }

        private void OnDisable()
        {
            player.Disable();
            MoveAction.performed -= SetMovement;
            MoveAction.canceled -= SetMovement;

            LookAction.performed -= SetLook;
            LookAction.canceled -= SetLook;

            ScrollAction.performed -= SetScroll;
            ScrollAction.canceled -= SetScroll;

            ZoomingAction.started -= SetZoom;
            ZoomingAction.canceled -= SetZoom;

            SprintAction.started -= SetRun;
            SprintAction.canceled -= SetRun;

            CrouchAction.started -= SetCrouch;
            CrouchAction.canceled -= SetCrouch;

            RotateObjectKeywordAction.started -= SetRotateObjectKeyboard;
            RotateObjectKeywordAction.canceled -= SetRotateObjectKeyboard;

            RotateLeftObjectControllerAction.started -= SetRotateObjectLeftHand;
            RotateLeftObjectControllerAction.canceled -= SetRotateObjectLeftHand;

            RotateRightObjectControllerAction.started -= SetRotateObjectRightHand;
            RotateRightObjectControllerAction.canceled -= SetRotateObjectRightHand;

            LeftArmAction.started -= InvokeLeftArmActions;
            LeftArmAction.started -= SetLeftArmActions;
            LeftArmAction.canceled -= SetLeftArmActions;

            RightArmAction.started -= InvokeRightArmActions;
            RightArmAction.started -= SetRightArmActions;
            RightArmAction.canceled -= SetRightArmActions;
        }

        void SetMovement(InputAction.CallbackContext ctx)
        {
            MovementInputDirection = ctx.ReadValue<Vector2>();
            IsPlayerMoving = !(MovementInputDirection == Vector2.zero);
        }

        void SetLook(InputAction.CallbackContext ctx)
        {
            LookInput = ctx.ReadValue<Vector2>();
            IsMovingCamera = !(LookInput == Vector2.zero);
        }

        void SetScroll(InputAction.CallbackContext ctx)
        {
            Scroll = ctx.ReadValue<Vector2>();
            IsScrolling = !(Scroll == Vector2.zero);
        }

        void SetRun(InputAction.CallbackContext ctx)
        {
            IsPlayerSprinting = ctx.started;
        }

        void SetCrouch(InputAction.CallbackContext ctx)
        {
            IsPlayerCrouching = ctx.started;
        }

        void SetRotateObjectKeyboard(InputAction.CallbackContext ctx)
        {
            IsRotateObjectKeyboardPressed = ctx.started;
        }

        void SetRotateObjectRightHand(InputAction.CallbackContext ctx)
        {
            IsRotateRightObjectControllerPressed = ctx.started;
        }

        void SetRotateObjectLeftHand(InputAction.CallbackContext ctx)
        {
            IsRotateLeftObjectControllerPressed = ctx.started;
        }

        void InvokeLeftArmActions(InputAction.CallbackContext ctx)
        {
            leftArmActions.Invoke();
        }
        void SetLeftArmActions(InputAction.CallbackContext ctx)
        {
            IsLeftArmPressed = ctx.started;
        }

        void InvokeRightArmActions(InputAction.CallbackContext ctx)
        {
            rightArmActions.Invoke();
        }

        void SetRightArmActions(InputAction.CallbackContext ctx)
        {
            IsRightArmPressed = ctx.started;
        }

        void SetZoom(InputAction.CallbackContext ctx)
        {
            IsZoomingCamera = ctx.started;
        }
        #endregion
    }
}