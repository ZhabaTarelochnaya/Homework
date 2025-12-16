using System;
using System.Threading.Tasks;
using NPCEventNotification.Scripts.Utils.Extansions;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NPCEventNotification.Scripts.Gameplay
{
    public class DayNightCycle
    {
        readonly EventManager _eventManager;
        readonly Light2D _globalLight;
        public float DayDuration = 15;
        public float NightDuration = 5;
        public float SwitchDuration = 2;
        public float NightLightLevel = 0.1f;
        public float DayLightLevel = 1f;
        

        public DayNightCycle(EventManager eventManager, Light2D globalLight)
        {
            _eventManager = eventManager;
            _globalLight = globalLight;
        }

        public async void StartCycle()
        {
            try
            {
                while (true)
                {
                    _eventManager.TriggerEvent(new GameEvent(GameEventName.Day, "Day started"));
                    await UnityTask.WaitForSeconds(DayDuration - SwitchDuration);
                    while (_globalLight.intensity >= NightLightLevel)
                    {
                        _globalLight.intensity -= (DayLightLevel -  NightLightLevel) * Time.deltaTime / SwitchDuration;
                        await Task.Yield();
                    }
                    _eventManager.TriggerEvent(new GameEvent(GameEventName.Night, "Night started"));
                    await UnityTask.WaitForSeconds(NightDuration - SwitchDuration);
                    while (_globalLight.intensity <= DayLightLevel)
                    {
                        _globalLight.intensity += (DayLightLevel -  NightLightLevel) * Time.deltaTime / SwitchDuration;
                        await Task.Yield();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}