using System.Collections.Generic;
using DefaultNamespace.Gameplay.Data.PickUp;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; } = 15f;
        [field: SerializeField] public float MouseSensitivity { get; private set; } = 2f;
        [field: SerializeField] public List<PickUpConfig> PickUps { get; private set; }
        [field: SerializeField] public float PickUpSpawnFrequency { get; private set; }
        

        public PlayerData CreatePlayerData()
        {
            var playerData = new PlayerData();
            playerData.Speed = PlayerSpeed;
            playerData.MouseSensitivity = MouseSensitivity;
            return playerData;
        }
    }
}