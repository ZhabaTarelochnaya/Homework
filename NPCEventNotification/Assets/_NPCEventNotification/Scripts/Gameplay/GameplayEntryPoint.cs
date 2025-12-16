using NPCEventNotification.Scripts.Gameplay.NPC;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NPCEventNotification.Scripts.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        EventManager eventManager;
        [SerializeField] NPCController npcController;
        [SerializeField] Light2D globalLight;
        public void Bind()
        {
            eventManager = new EventManager();
            if (!globalLight) Debug.LogError("GameplayEntryPoint: globalLight is null");
            var dayNightCycle = new DayNightCycle(eventManager, globalLight);
            
            if (!npcController) Debug.LogError("GameplayEntryPoint: npcController is null");
            npcController.Bind(eventManager);
            
            dayNightCycle.StartCycle();
        }
    }
}