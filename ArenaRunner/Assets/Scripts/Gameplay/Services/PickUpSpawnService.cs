using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.PickUp;
using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;

namespace DefaultNamespace.Gameplay.World
{
    public class PickUpSpawnService : IService
    {
        readonly EventBus _eventBus;
        readonly List<IPickUpConfig> _configs;
        readonly List<PickUpDataProxy> _pickUps = new ();
        public IEnumerable<PickUpDataProxy>  PickUps => _pickUps;
        public PickUpSpawnService(List<IPickUpConfig> configs)
        {
            _configs = configs;
            _eventBus = ServiceLocator.Current.Get<EventBus>();
        }

        public PickUpDataProxy SpawnAtPosition(PickUpType pickUpType, Vector3 position)
        {
            var config = _configs.FirstOrDefault(p => p.Type == pickUpType);
            if (config == null) throw new Exception($"Pick up config of type {pickUpType} not found");

            var pickUpData = config.Create();
            pickUpData.Position = position;

            var pickUpDataProxy = new PickUpDataProxy(pickUpData);
            _pickUps.Add(pickUpDataProxy);

            _eventBus.TriggerEvent(new GameEvent(EventName.PickUpCreated,
                $"PickUp {pickUpType} created at {position}",
                pickUpDataProxy));

            return pickUpDataProxy;
        }

        public bool DestroyPickUp(int pickUpID)
        {
            var pickUpIndex = _pickUps.FindIndex(p => p.ID == pickUpID);
            if (pickUpIndex < 0) return false;
            _pickUps.RemoveAt(pickUpIndex);
            
            _eventBus.TriggerEvent(new GameEvent(EventName.PickUpDestroyed, 
                $"Pickup with id{pickUpID} destroyed",
                pickUpIndex));
            
            return true;
        }
    }
}