using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.View.Camera
{
    public class CameraViewModel
    {
        readonly PlayerDataProxy _playerDataProxy;
        readonly CameraManager _cameraManager;
        public Vector3 Position { get =>  _playerDataProxy.Position; }
        public Vector3 CameraRotation
        {
            get => _playerDataProxy.CameraRotation;
            set => _playerDataProxy.CameraRotation = value;
        }

        public CameraViewModel(PlayerDataProxy playerDataProxy)
        {
            _playerDataProxy = playerDataProxy;
            _cameraManager = ServiceLocator.Current.Get<CameraManager>();
        }

        public void LateUpdate() => _cameraManager.LateUpdate();
    }
}