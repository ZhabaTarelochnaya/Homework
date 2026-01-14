using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.Data.PickUp;
using DefaultNamespace.Gameplay.World;
using UnityEditor;
using UnityEngine;
using Utils.EventBus;
using Utils.ServiceLocator;
using Object = UnityEngine.Object;

namespace DefaultNamespace.Gameplay.View.PickUp
{
    public class PickUpSpawnerViewModel
    {
        readonly GameConfig _gameConfig;
        public event Action<GameObject, PickUpViewModel> Spawned;
        public event Action<int> Destroyed;

        public PickUpSpawnerViewModel(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            var pickUpService = ServiceLocator.Current.Get<PickUpSpawnService>();
            var eventBus = ServiceLocator.Current.Get<EventBus>();
            eventBus.OnGameEvent += OnPickUp;
        }

        void OnPickUp(GameEvent e)
        {
            if (e.Name == EventName.PickUpCreated)
            {
                var pickUpDataProxy = (PickUpDataProxy)e.Args[0];
                var pickUpViewModel = new PickUpViewModel(pickUpDataProxy);
                var config = _gameConfig.PickUps
                    .FirstOrDefault(c => c.Type == pickUpDataProxy.Type);
                Spawned?.Invoke(config.Prefab, pickUpViewModel);
            }
            else if (e.Name == EventName.PickUpDestroyed)
            {
                int pickUpIndex = (int)e.Args[0];
                Destroyed?.Invoke(pickUpIndex);
            }
        }
    }
}