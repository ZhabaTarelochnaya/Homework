using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.Camera;
using DefaultNamespace.Gameplay.Services;
using DefaultNamespace.Gameplay.World;
using Gameplay.Services;
using Gameplay.Services.Camera;
using UnityEngine;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Controllers
{
    public class CameraManager : IService
    {
        public bool IsFollowingTarget = true;
        public ICameraUser CurrentTarget { get; set; }
        public ICamera CurrentCamera { get; set; }
        readonly ICameraUser _player;
        
        public CameraManager(PlayerState playerState, ICamera camera)
        {
            _player = playerState;
            CurrentTarget =  _player;
            CurrentCamera = camera;
        }

        public void FollowPlayer() => CurrentTarget = _player;
        public void LateUpdate()
        {
            if (!IsFollowingTarget) return;
            FollowTarget(CurrentTarget, CurrentCamera);
        }

        void FollowTarget(ICameraUser currentTarget, ICamera currentCamera)
        {
            currentCamera.Position = _player.Position;
            currentCamera.Rotation = currentTarget.CameraRotation;
        }
    }
}