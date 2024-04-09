using MFPC.Input.PlayerInput;
using UnityEngine;
using Cinemachine;

namespace MFPC.Camera
{
    public class RunFovCameraEffect : ICameraModule
    {
        private UnityEngine.Camera _camera;
        private IPlayerInput _playerInput;
        private PlayerData _playerData;
        private Player _player;
        private float _initialFov;
        private float _time;
        CinemachineVirtualCamera _virtualCamera;

        public RunFovCameraEffect(UnityEngine.Camera camera, IPlayerInput playerInput, PlayerData playerData,
            Player player, CinemachineVirtualCamera virtualCamera)
        {
            _camera = camera;
            _playerInput = playerInput;
            _playerData = playerData;
            _player = player;
            _virtualCamera = virtualCamera;

            if (_playerData.UsingCinemachine)
                _initialFov = _virtualCamera.m_Lens.FieldOfView;
            else
                _initialFov = _camera.fieldOfView;
        }

        public void Update()
        {
            if (_playerData.UsingCinemachine)
            {
                if (_virtualCamera == null) return;
            }
            else
            {
                if (_camera == null) return;
            }

            _time += (Time.deltaTime * _playerData.SpeedChangeFOV) * (IsIncreaseFOV() ? 1 : -1);
            _time = Mathf.Clamp01(_time);

            if (_playerData.UsingCinemachine)
                _virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_initialFov, _playerData.RunFOV, _time);
            else
                _camera.fieldOfView = Mathf.Lerp(_initialFov, _playerData.RunFOV, _time);
        }

        private bool IsIncreaseFOV() => _playerInput.IsSprint && _player.CurrentMoveCondition != MoveConditions.Climb
                                                              && _player.CurrentMoveCondition != MoveConditions.Fell
                                                              && _player.CurrentMoveCondition != MoveConditions.Lean;
    }
}