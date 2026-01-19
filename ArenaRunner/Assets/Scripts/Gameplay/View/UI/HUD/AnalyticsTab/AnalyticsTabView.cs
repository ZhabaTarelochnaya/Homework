using TMPro;
using UnityEngine;
using Utils.EventBus;

namespace DefaultNamespace.AnalyticsTab
{
    public class AnalyticsTabView : MonoBehaviour
    {
        AnalyticsTabViewModel _viewModel;
        [SerializeField] TMP_Text _text;
        [SerializeField] GameObject _analyticsPanel;
        [SerializeField, Range(0,10)] int _lastEventsNumber;
        
        public void Bind(AnalyticsTabViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnGameEvent += EventManagerOnOnOnGameEvent;
        }
        void EventManagerOnOnOnGameEvent(GameEvent e)
        {
            if (!_analyticsPanel.activeSelf) return;
            _text.text = _viewModel.GetEnemySpawnedCount() + "\n";
            _text.text += _viewModel.GetPickedItemsCount() + "\n";
            _text.text += _viewModel.GetLastEvents(_lastEventsNumber);
        }
        public void OnShowAnalyticsButtonDown()
        {
            _analyticsPanel.SetActive(!_analyticsPanel.activeSelf);
            _text.text = _viewModel.GetLastEvents(_lastEventsNumber);
        }
    }
}
