using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Controllers
{
    public interface ICameraManager : IService
    {
        public Camera CurrentCamera { get; set; }
        public void FollowPosition(Transform target);

        public void FollowRotation(float lookY, float playerYRotation);
    }
}