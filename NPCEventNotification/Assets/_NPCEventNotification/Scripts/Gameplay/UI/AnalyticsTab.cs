using System.Linq;
using TMPro;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.UI
{
    public class AnalyticsTab : MonoBehaviour
    {
        EventManager _eventManger;
        [SerializeField] TMP_Text _text;
        [SerializeField] GameObject _analyticsPanel;
        
        public void Bind(EventManager eventManager)
        {
            if (!_text) Debug.LogError($"{gameObject.name}: _text is not set");
            if (!_analyticsPanel) Debug.LogError($"{gameObject.name}: _analyticsPanel is not set");
            _eventManger = eventManager;
            _eventManger.OnGameEvent += EventManagerOnOnGameEvent;
        }

        void EventManagerOnOnGameEvent(GameEvent e)
        {
            if (!_analyticsPanel.activeSelf) return;
            UpdateAnalyticsPanel();
        }
        public void OnShowAnalyticsButtonDown()
        {
            _analyticsPanel.SetActive(!_analyticsPanel.activeSelf);
            UpdateAnalyticsPanel();
        }

        void UpdateAnalyticsPanel()
        {
            var gameEvents = _eventManger.GameEvents;
            string result;
            var daysPassed = gameEvents
                .Count(e => e.Name == GameEventName.Morning);
            result = $"Days passed: {--daysPassed}\n";
            
            var enemiesKilled = gameEvents
                .Count(e => e.Name == GameEventName.EnemyKilled);
            result += $"Enemies killed: {enemiesKilled}\n";

            var mostOccuringEvent = gameEvents.GroupBy(e => e.Name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(e => e.Count)
                .FirstOrDefault();
            if (mostOccuringEvent != null)
            {
                result += $"Most occuring event ({mostOccuringEvent.Count} times): {mostOccuringEvent.Name}\n";
            }
            
            result += "Last events:\n";
            gameEvents.TakeLast(5)
                .ToList()
                .ForEach(e => result += $"{e}\n");

            _text.text = result;
        }
    }
}