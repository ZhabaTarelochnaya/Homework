using System;
using System.Collections.Generic;
using DefaultNamespace.Gameplay.Data.Camera;
using DefaultNamespace.Gameplay.Data.Enemy;
using DefaultNamespace.Gameplay.Data.PickUp;

namespace DefaultNamespace.Gameplay.Data
{
    [Serializable]
    public class GameData
    {
        public int ID;
        public int Score;
        public GameStateName GameStateName;
        public List<PickUpData> PickUps = new();
        public List<EnemyData> Enemies = new();
        public PlayerData PlayerData;
        public CameraData CameraData;
    }
}