using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.UI
{
    public class SimulationSpeedSlider : MonoBehaviour
    {
        public void OnValueChanged(float value)
        {
            Time.timeScale = value;
        }
    }
}