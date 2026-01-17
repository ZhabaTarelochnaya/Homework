using DefaultNamespace.Gameplay.World;
using Gameplay.Services.Camera;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.Controllers
{
    public class CameraManager : IService
    {
        readonly IPositionUser _cameraUser;
        public bool IsFollowingTarget { get; set; } = true;
        public IPositionUser CurrentTarget { get; set; }
        public ICamera CurrentCamera { get; set; }
        
        public CameraManager(IPositionUser cameraUser, ICamera camera)
        {
            _cameraUser = cameraUser;
            CurrentTarget =  _cameraUser;
            CurrentCamera = camera;
        }

        public void FollowPlayer() => CurrentTarget = _cameraUser;
        public void LateUpdate()
        {
            if (!IsFollowingTarget) return;
            FollowTarget(CurrentCamera);
        }

        void FollowTarget(ICamera currentCamera)
        {
            currentCamera.Position = _cameraUser.Position;
        }
    }
}