using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC
{
    [CreateAssetMenu(fileName = "WorkerData", menuName = "ScriptableObjects/WorkerData")]
    public class WorkerDataSO : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; }

        public void Fill(WorkerData data)
        {
            data.MaxHealth = MaxHealth;
        }
    }
}