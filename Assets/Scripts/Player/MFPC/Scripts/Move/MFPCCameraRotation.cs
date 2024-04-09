using System.Collections.Generic;
using MFPC.Camera;
using MFPC.Input;
using UnityEngine;
using MFPC.Input.PlayerInput;
using MFPC.Utils;
using Singletons;
using Managers;
using Cinemachine;

namespace MFPC
{
    /// <summary>
    /// Allows you to rotate the camera vertically
    /// </summary>
    public class MFPCCameraRotation : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _virtualCamera;
        public CameraModuleManager CameraModuleManager { get; private set; }

        private float _lookDirection;
        private IPlayerInput _playerInput;
        private SensitiveData _sensitiveData;
        private PlayerData _playerData;

        InputManager _inputs;

        private void Start()
        {
            _inputs = MasterSingleton.Instance.InputManager;
        }
        private void Update()
        {
            CameraModuleManager?.Update();

            if (_inputs.IsRotateLeftObjectControllerPressed == false
                && _inputs.IsRotateRightObjectControllerPressed == false
                && _inputs.IsRotateObjectKeyboardPressed == false)
            {
                _lookDirection += _playerInput.CalculatedVerticalLookDirection;
                _lookDirection = Mathf.Clamp(_lookDirection,
                    _playerData.RangeCameraRotationVertical.x,
                    _playerData.RangeCameraRotationVertical.y);

                this.transform.localRotation = RotateHelper.SmoothRotateVertical(this.transform.localRotation,
                    _sensitiveData.RotateSpeedSmoothVertical, _lookDirection);
            }
        }

        public void Initialize(PlayerInputTuner playerInputTuner, SensitiveData sensitiveData, PlayerData playerData, Player player)
        {
            _playerInput = player.Input;
            _sensitiveData = sensitiveData;
            _playerData = playerData;

            CameraModuleManager = new CameraModuleManager(this.transform, playerData, player, _virtualCamera);
        }

        public void SetRotation(float angle)
        {
            _lookDirection = Mathf.Clamp(angle,
                _playerData.RangeCameraRotationVertical.x,
                _playerData.RangeCameraRotationVertical.y);
        }
    }
}