using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class GameEndPopup : MonoBehaviour, IPopup
    {
        GameplayStateManager _gameplayStateManager;

        public void Bind()
        {
            _gameplayStateManager = ServiceLocator.Current.Get<GameplayStateManager>();
        }
        public void OnRestartButtonPressed() => _gameplayStateManager.Reload();
    }
}