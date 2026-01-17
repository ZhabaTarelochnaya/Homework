using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Gameplay.Data.PickUp;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; } = 15f;
        [field: SerializeField] public List<PickUpConfig> PickUps { get; private set; }
        [field: SerializeField] public List<EnemyConfig> EnemyConfigs { get; private set; }
        [field: SerializeField] public float EnemySpawnDelay { get; private set; }
        [field : SerializeField] public UIConfig UIConfig { get; private set; }
        

        public PlayerData CreatePlayerData()
        {
            var playerData = new PlayerData();
            playerData.Speed = PlayerSpeed;
            return playerData;
        }

        public PickUpConfig GetPickUpConfig(PickUpType type)
        {
            return PickUps.FirstOrDefault(c => c.Type == type);
        }
    }
}