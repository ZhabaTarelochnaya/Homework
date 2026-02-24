using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Move
{
    public interface IMovementService: IService
    {
        public void AddRun(Vector2 direction);
        public void AddJump();
        public void Move();
    }
}