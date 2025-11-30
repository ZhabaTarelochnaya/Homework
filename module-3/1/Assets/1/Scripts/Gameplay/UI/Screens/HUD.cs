using _1.Gameplay.Data;
using _1.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _1.Gameplay.UI
{
    public class HUD : MonoBehaviour
    {
        IDisposableBag _disposableBag = new ();
        int _recordTargetsHit;
        GameplayRequests _gameplayRequests;
        [SerializeField] TMP_Text _score;
        [SerializeField] TMP_Text _record;
        
        public void Bind(GameplayDataProxy dataProxy, GameplayRequests gameplayRequests, int recordTargetsHit)
        {
            _gameplayRequests = gameplayRequests;
            _recordTargetsHit = recordTargetsHit;
            _record.text = recordTargetsHit.ToString();
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
            _score.text = $"{targetsHit} / {shotsFired}";
            if (targetsHit > _recordTargetsHit)
            {
                _record.text = targetsHit.ToString();
            }
        }
        
        void OnDestroy()
        {
            _disposableBag.Dispose();
        }
    }
}