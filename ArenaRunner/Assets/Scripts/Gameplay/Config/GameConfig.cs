using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; } = 15f;
        [field: SerializeField] public float MouseSensitivity { get; private set; } = 2f;

        public PlayerData CreatePlayerData()
        {
            var playerData = new PlayerData();
            playerData.Speed = PlayerSpeed;
            playerData.MouseSensitivity = MouseSensitivity;
            return playerData;
        }
    }
}