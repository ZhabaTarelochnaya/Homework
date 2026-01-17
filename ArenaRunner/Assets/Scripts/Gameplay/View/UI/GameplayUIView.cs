using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace
{
    public class GameplayUIView : MonoBehaviour
    {
        [SerializeField] RectTransform _screens;
        [SerializeField] RectTransform _popUps;

        public void Bind(GameplayUIViewModel gameplayUIViewModel)
        {
            gameplayUIViewModel.SetContainers(_screens, _popUps);
            gameplayUIViewModel.OpenHUD();
        }
    }
}