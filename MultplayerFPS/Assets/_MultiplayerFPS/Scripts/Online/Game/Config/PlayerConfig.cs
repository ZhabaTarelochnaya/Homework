using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; } = 10f;
        [field: SerializeField] public float JumpHeight { get; private set; } = 5f;
        [field: SerializeField] public float Gravity { get; private set; } = 1f;
        [field: SerializeField] public float RespawnTime { get; private set; } = 3;
        [field: SerializeField] public int MaxHealth { get; private set; } = 100;
        [field: SerializeField] public ScriptableObject StartingWeapon { get; private set; }
    }
}