using System.Linq;
using TMPro;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] AnalyticsTab _analyticsTab;
        public void Bind(EventManager eventManager)
        {
            if (!_analyticsTab) Debug.LogError($"{gameObject.name}: _analyticsTab is not set");
            _analyticsTab.Bind(eventManager);
        }
    }
}