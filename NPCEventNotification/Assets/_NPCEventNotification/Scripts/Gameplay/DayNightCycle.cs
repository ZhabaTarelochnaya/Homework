using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NPCEventNotification.Scripts.Gameplay
{
    public class DayNightCycle
    {
        readonly EventManager _eventManager;
        readonly Light2D _globalLight;
        public float DayDuration { get; set; } = 15;
        public float NightDuration { get; set; } = 5;
        public float SwitchDuration { get; set; } = 2;
        public float NightLightLevel { get; set; } = 0.1f;
        public float DayLightLevel  { get; set; } = 1f;
        public DayNightCycle(EventManager eventManager, Light2D globalLight)
        {
            _eventManager = eventManager;
            _globalLight = globalLight;
        }

        public IEnumerator StartCycle()
        {
            while (true)
            {
                _eventManager.TriggerEvent(new GameEvent(GameEventName.Day, "Day started"));
                yield return new WaitForSeconds(DayDuration - SwitchDuration);
                while (_globalLight.intensity >= NightLightLevel)
                {
                    _globalLight.intensity -= (DayLightLevel -  NightLightLevel) * Time.deltaTime / SwitchDuration;
                    yield return null;
                }
                _eventManager.TriggerEvent(new GameEvent(GameEventName.Night, "Night started"));
                yield return new WaitForSeconds(NightDuration - SwitchDuration);
                while (_globalLight.intensity <= DayLightLevel)
                {
                    _globalLight.intensity += (DayLightLevel -  NightLightLevel) * Time.deltaTime / SwitchDuration;
                    yield return null;
                }
            }
        }
    }
}