using NPCEventNotification.Scripts.Gameplay.NPC;
using NPCEventNotification.Scripts.Gameplay.World.NPC.Types.Warden;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.World.NPC
{
    [CreateAssetMenu(fileName = "AllyFactory", menuName = "ScriptableObjects/AllyFactory")]
    public class AllyFactorySO : ScriptableObject
    {
        [SerializeField] GameObject _workerPrefab;
        [SerializeField] GameObject _guardPrefab;
        
        public WorkerController CreateWorker(Vector2 position)
        {
            if (!_workerPrefab) Debug.LogError("AllyFactorySO: No worker prefab found");
            var instance = Instantiate(_workerPrefab, position,  Quaternion.identity);
            return instance.GetComponent<WorkerController>();
        }
        public GuardController CreateGuard(Vector2 position)
        {
            if (!_guardPrefab) Debug.LogError("AllyFactorySO: No guard prefab found");
            var instance = Instantiate(_guardPrefab, position,  Quaternion.identity);
            return instance.GetComponent<GuardController>();
        }
    }
}