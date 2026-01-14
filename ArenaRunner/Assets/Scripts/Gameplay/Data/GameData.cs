using System;
using System.Collections.Generic;
using DefaultNamespace.Gameplay.Data.Camera;
using DefaultNamespace.Gameplay.Data.PickUp;

namespace DefaultNamespace.Gameplay.Data
{
    [Serializable]
    public class GameData
    {
        public int ID;
        public List<PickUpData> PickUps = new();
        public PlayerData PlayerData;
        public CameraData CameraData;
    }
}