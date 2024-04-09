using MFPC.Input;
using MFPC.Input.PlayerInput;
using MFPC.Utils;
using UnityEngine;
using Managers;

namespace MFPC
{
    public class MFPCPlayerRotation
    {
        public float SetRotation { set => LookDirection = value; }

        /// <summary>
        /// The direction the player is facing (Vertical)
        /// </summary>
        private float LookDirection;

        private IPlayerInput _playerInput;
        private PlayerInputTuner _playerInputTuner;
        private SensitiveData _sensitiveData;
        private Transform _playerTransform;
        InputManager _inputs;

        public MFPCPlayerRotation(Transform playerTransform, IPlayerInput playerInput,
            PlayerInputTuner playerInputTuner, SensitiveData sensitiveData, InputManager inputManager)
        {
            _playerTransform = playerTransform;
            _playerInput = playerInput;
            _playerInputTuner = playerInputTuner;
            _sensitiveData = sensitiveData;
            _inputs = inputManager;

            LookDirection = _playerTransform.rotation.eulerAngles.y;
        }

        public void UpdatePlayerRotation()
        {
            if (_inputs.IsRotateLeftObjectControllerPressed == false
                && _inputs.IsRotateRightObjectControllerPressed == false
                && _inputs.IsRotateObjectKeyboardPressed == false)
            {
                LookDirection += _playerInput.CalculatedHorizontalLookDirection;
                _playerTransform.localRotation = RotateHelper.SmoothRotateHorizontal(_playerTransform.localRotation,
                    _sensitiveData.RotateSpeedSmoothHorizontal, LookDirection);
            }
        }
    }
}