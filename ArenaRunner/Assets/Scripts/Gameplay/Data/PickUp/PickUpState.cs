using System;
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
        public Vector2 Position { get; set; }
        public int ID { get; }
        public event Action Collected;

        public PickUpState(PickUpData pickUpData)
        {
            Type = pickUpData.Type;
            Score = pickUpData.Score;
            Position = pickUpData.Position;
            ID = pickUpData.ID;
        }
        public PickUpData ToData()
        {
            var pickUpData = new PickUpData();
            pickUpData.Type = Type;
            pickUpData.Score = Score;
            pickUpData.Position = Position;
            pickUpData.ID = ID;
            return pickUpData;

        }
        public void Collect() => Collected?.Invoke();
    }
}