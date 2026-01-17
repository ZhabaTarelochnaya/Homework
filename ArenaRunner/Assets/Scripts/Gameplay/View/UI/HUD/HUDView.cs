using DefaultNamespace.AnalyticsTab;
using TMPro;
using UnityEngine;
using Utils.EventBus;

namespace DefaultNamespace
{
    public class HUDView : MonoBehaviour
    {
        HUDViewModel _hudViewModel;
        [SerializeField] AnalyticsTabView _analyticsTab;
        [SerializeField] TMP_Text _score;
        public void Bind(HUDViewModel hudViewModel)
        {
            _hudViewModel = hudViewModel;
            _analyticsTab.Bind(_hudViewModel.AnalyticsTabViewModel);
            _hudViewModel.OnGameEvent += HudViewModelOnGameEvent;
        }

        void HudViewModelOnGameEvent(GameEvent e)
        {
            if (e.Name == EventName.ScoreChanged)
            {
                int score = (int)e.Args[0];
                _score.text = $"Score: {score}";
            }
        }
    }
}