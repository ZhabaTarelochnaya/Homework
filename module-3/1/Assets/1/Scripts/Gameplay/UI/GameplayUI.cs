using _1.Gameplay.Data;
using UnityEngine;

namespace _1.Gameplay.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] GameObject _hud;
        [SerializeField] Transform _screens;
        [SerializeField] Transform _popUps;
        public void Bind(GameplayRequests gameplayRequests, GameplayDataProxy dataProxy, int recordTargetsHit)
        {
            var hudInstance = Instantiate(_hud, _screens);
            var hud = hudInstance.GetComponent<HUD>();
            
            hud.Bind(dataProxy, gameplayRequests, recordTargetsHit);

            gameplayRequests.ReloadGameplayRequested += Destroy;
            gameplayRequests.LoadMainMenuRequested += Destroy;
        }
        void Destroy()
        {
            Destroy(gameObject);
        }
    }
}