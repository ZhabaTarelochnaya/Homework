using System;
using _NPCEventNotification.Scripts.Gameplay.World.NPC.Types;
using NPCEventNotification.Scripts.Gameplay.NPC;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using NPCEventNotification.Scripts.Gameplay.World;
using NPCEventNotification.Scripts.Gameplay.World.NPC;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NPCEventNotification.Scripts.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        EventManager eventManager;
        [SerializeField] EnemySpawner _enemySpawner;
        [SerializeField] Light2D _globalLight;
        [SerializeField] AllyFactorySO _allyFactory;
        [SerializeField] TownHall _townHall;
        [SerializeField] Transform _storage;
        [SerializeField] ResourceZone _resourceZone;

        void Awake()
        {
            if (!_globalLight) Debug.LogError("GameplayEntryPoint: _globalLight is null");
            if (!_enemySpawner) Debug.LogError("GameplayEntryPoint: _enemySpawner is null");
            if (!_allyFactory) Debug.LogError("GameplayEntryPoint: _allyFactory is null");
            if (!_townHall) Debug.LogError("GameplayEntryPoint: _townHall is null");
        }

        public void Bind()
        {
            eventManager = new EventManager();
            var dayNightCycle = new DayNightCycle(eventManager, _globalLight);
            var allyFactory = new AllyFactory(_allyFactory,eventManager, _resourceZone, _townHall, _storage);
            
            _townHall.Bind(allyFactory);
            _enemySpawner.Bind(eventManager);
            
            StartCoroutine(dayNightCycle.StartCycle());
        }
    }
}