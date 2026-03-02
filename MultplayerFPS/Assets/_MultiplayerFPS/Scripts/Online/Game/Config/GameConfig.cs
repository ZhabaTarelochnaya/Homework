using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public bool DoPickupsRespawn { get; private set; } = true;
        [field: SerializeField] public float PickupRespawnTime { get; private set; } = 10f;
        [field: SerializeField] public float MaxPickups { get; private set; } = 4f;
        [field: SerializeField] public int MatchDuration { get; private set; } = 300;
    }
}