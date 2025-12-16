using NPCEventNotification.Scripts.Gameplay.NPC;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NPCEventNotification.Scripts.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        EventManager eventManager;
        [SerializeField] WorkerController workerController;
        [SerializeField] Light2D globalLight;
        public void Bind()
        {
            eventManager = new EventManager();
            if (!globalLight) Debug.LogError("GameplayEntryPoint: globalLight is null");
            var dayNightCycle = new DayNightCycle(eventManager, globalLight);
            
            if (!workerController) Debug.LogError("GameplayEntryPoint: npcController is null");
            workerController.Bind(eventManager);
            
            dayNightCycle.StartCycle();
        }
    }
}