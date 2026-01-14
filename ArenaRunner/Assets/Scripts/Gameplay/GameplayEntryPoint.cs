using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.Camera;
using DefaultNamespace.Gameplay.View;
using DefaultNamespace.Gameplay.View.Camera;
using DefaultNamespace.Gameplay.View.PickUp;
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
        [SerializeField] Transform _pickUps;
        public void Bind(GameConfig gameConfig, GameState gameState)
        {
            isBound = true;
            
            var playerData = gameConfig.CreatePlayerData();
            var playerState = new PlayerState(playerData);

            var cameraData = new CameraData();
            var cameraState = new CameraState(cameraData);
            
            GameplayServiceRegistrations.Register(playerState, cameraState, gameConfig);
            
            var playerController = new PlayerController(playerState);
            
            var playerViewModel = new PlayerViewModel(playerState, playerController);
            _playerView.Bind(playerViewModel);
            var cameraViewModel = new CameraViewModel(playerState);
            _cameraView.Bind(cameraViewModel);

            BindPickUps(gameConfig, gameState);
        }

        void OnDestroy()
        {
            if (!isBound) return;
            GameplayServiceRegistrations.Unregister();
        }

        void BindPickUps(GameConfig gameConfig, GameState gameState)
        {
            foreach (var pickUp in _pickUps.GetComponentsInChildren<PickUpView>())
            {
                var config = gameConfig.GetPickUpConfig(pickUp.PickUpType);
                var pickUpState = config.Create();
                gameState.PickUps.Add(pickUpState);
                var pickUpViewModel = new PickUpViewModel(pickUpState);
                pickUp.Bind(pickUpViewModel);
            }
        }
    }
}