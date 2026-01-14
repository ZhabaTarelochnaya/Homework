using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.View.Camera
{
    public class CameraViewModel
    {
        readonly PlayerState _playerState;
        readonly CameraManager _cameraManager;
        public Vector3 Position { get =>  _playerState.Position; }
        public Quaternion CameraRotation
        {
            get => _playerState.CameraRotation;
            set => _playerState.CameraRotation = value;
        }

        public CameraViewModel(PlayerState playerState)
        {
            _playerState = playerState;
            _cameraManager = ServiceLocator.Current.Get<CameraManager>();
        }

        public void LateUpdate() => _cameraManager.LateUpdate();
    }
}