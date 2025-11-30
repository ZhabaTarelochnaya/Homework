using _1.Gameplay.Data;
using _1.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _1.Gameplay.UI
{
    public class HUD : MonoBehaviour
    {
        IDisposableBag _disposableBag = new ();
        [SerializeField] TMP_Text _text;
        public void Bind(GameplayDataProxy dataProxy)
        {
            _disposableBag.Add(dataProxy.ShotsFired
                .Subscribe(i => UpdateScore(dataProxy.TargetsHit.Value, i)));
            _disposableBag.Add(dataProxy.TargetsHit
                .Subscribe(i => UpdateScore(i, dataProxy.ShotsFired.Value)));
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