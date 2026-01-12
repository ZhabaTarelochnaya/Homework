using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.World;
using DefaultNamespace.Gameplay.World.Player;
using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        bool isBound;
        [SerializeField] PlayerView _playerView;
        public void Bind(GameConfig gameConfig)
        {
            isBound = true;
            GameplayServiceRegistrations.Register();
            
            var playerData = gameConfig.CreatePlayerData();
            var playerDataProxy = new PlayerDataProxy(playerData);
            var playerController = new PlayerController(playerDataProxy);
            
            
            var playerViewModel = new PlayerViewModel(playerDataProxy, playerController);
            _playerView.Bind(playerViewModel);
        }

        void OnDestroy()
        {
            if (!isBound) return;
            GameplayServiceRegistrations.Unregister();
        }
    }
}