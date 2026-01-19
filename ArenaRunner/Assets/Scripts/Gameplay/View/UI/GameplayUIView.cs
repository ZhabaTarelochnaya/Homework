using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace
{
    public class GameplayUIView : MonoBehaviour
    {
        GameplayUIViewModel _gameplayUIViewModel;
        [SerializeField] RectTransform _screens;
        [SerializeField] RectTransform _popUps;

        public void Bind(GameplayUIViewModel gameplayUIViewModel)
        {
            _gameplayUIViewModel = gameplayUIViewModel;
            SetContainers();
            gameplayUIViewModel.OpenHUD();
        }
        public void SetContainers() => _gameplayUIViewModel.SetContainers(_screens, _popUps);
    }
}