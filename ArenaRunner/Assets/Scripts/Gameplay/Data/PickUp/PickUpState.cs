using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace DefaultNamespace.Gameplay.Data.PickUp
{
    public class PickUpState : IPositionUser
    {
        public PickUpType Type { get; }
        public int Score { get; }
        public Vector3 Position { get; set; }
        public int ID { get; }
        public GameObject Prefab { get; }

        public PickUpState(PickUpData pickUpData, GameConfig gameConfig)
        {
            Type = pickUpData.Type;
            Score = pickUpData.Score;
            Position = pickUpData.Position;
            ID = pickUpData.ID;
            var config = gameConfig.PickUps.FirstOrDefault(p => p.Type == Type);
            Prefab = config?.Prefab;
        }
        public PickUpState(PickUpData pickUpData, PickUpConfig pickUpConfig)
        {
            Type = pickUpData.Type;
            Score = pickUpData.Score;
            Position = pickUpData.Position;
            ID = pickUpData.ID;
            Prefab = pickUpConfig?.Prefab;
        }
    }
}