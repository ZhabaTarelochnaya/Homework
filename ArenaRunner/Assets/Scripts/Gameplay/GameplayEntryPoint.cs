using DefaultNamespace.Gameplay.Controllers;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.Camera;
using DefaultNamespace.Gameplay.View;
using DefaultNamespace.Gameplay.View.Camera;
using DefaultNamespace.Gameplay.World;
using DefaultNamespace.Gameplay.World.Player;
using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        bool isBound;
        [SerializeField] PlayerView _playerView;
        [SerializeField] CameraView _cameraView;
        public void Bind(GameConfig gameConfig)
        {
            isBound = true;
            
            var playerData = gameConfig.CreatePlayerData();
            var playerDataProxy = new PlayerDataProxy(playerData);

            var cameraData = new CameraData();
            var cameraDataProxy = new CameraDataProxy(cameraData);
            
            GameplayServiceRegistrations.Register(playerDataProxy, cameraDataProxy, gameConfig);
            
            var playerController = new PlayerController(playerDataProxy);
            
            var playerViewModel = new PlayerViewModel(playerDataProxy, playerController);
            _playerView.Bind(playerViewModel);
            var cameraViewModel = new CameraViewModel(playerDataProxy);
            _cameraView.Bind(cameraViewModel);
        }

        void OnDestroy()
        {
            if (!isBound) return;
            GameplayServiceRegistrations.Unregister();
        }
    }
}