using System;
using System.Collections.Generic;
using DefaultNamespace.Gameplay.Data.PickUp;

namespace DefaultNamespace.Gameplay.Data
{
    [Serializable]
    public class GameData
    {
        public List<PickUpData> PickUps = new();
        
    }
}