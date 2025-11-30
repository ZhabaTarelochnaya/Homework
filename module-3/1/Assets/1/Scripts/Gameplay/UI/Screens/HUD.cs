using _1.Gameplay.Data;
using _1.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _1.Gameplay.UI
{
    public class HUD : MonoBehaviour
    {
        IDisposableBag _disposableBag = new ();
        GameplayRequests _gameplayRequests;
        [SerializeField] TMP_Text _text;
        
        public void Bind(GameplayDataProxy dataProxy, GameplayRequests gameplayRequests)
        {
            _gameplayRequests = gameplayRequests;
            _disposableBag.Add(dataProxy.ShotsFired
                .Subscribe(i => UpdateScore(dataProxy.TargetsHit.Value, i)));
            _disposableBag.Add(dataProxy.TargetsHit
                .Subscribe(i => UpdateScore(i, dataProxy.ShotsFired.Value)));
        }

        public void ToMenuButtonPressed()
        {
            _gameplayRequests.LoadMainMenuRequested.Invoke();
        }
        void UpdateScore(int targetsHit, int shotsFired)
        {
            _text.text = $"{targetsHit} / {shotsFired}";
        }
        
        void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}