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
        public Vector2 Position => _playerState.Position;

        public CameraViewModel(PlayerState playerState)
        {
            _playerState = playerState;
            _cameraManager = ServiceLocator.Current.Get<CameraManager>();
        }

        public void LateUpdate() => _cameraManager.LateUpdate();
    }
}