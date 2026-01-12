using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; }

        public PlayerData CreatePlayerData()
        {
            var playerData = new PlayerData();
            playerData.Speed = PlayerSpeed;
            return playerData;
        }
    }
}