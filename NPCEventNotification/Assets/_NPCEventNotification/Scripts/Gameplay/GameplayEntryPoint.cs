using System;
using NPCEventNotification.Scripts.Gameplay.NPC;
using NPCEventNotification.Scripts.Gameplay.World;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NPCEventNotification.Scripts.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        EventManager eventManager;
        [SerializeField] WorkerController _workerController;
        [SerializeField] EnemySpawner _enemySpawner;
        [SerializeField] Light2D _globalLight;

        void Awake()
        {
            if (!_globalLight) Debug.LogError("GameplayEntryPoint: _globalLight is null");
            if (!_workerController) Debug.LogError("GameplayEntryPoint: _workerController is null");
            if (!_enemySpawner) Debug.LogError("GameplayEntryPoint: _enemySpawner is null");
        }

        public void Bind()
        {
            eventManager = new EventManager();
            var dayNightCycle = new DayNightCycle(eventManager, _globalLight);
            
            _workerController.Bind(eventManager);
            _enemySpawner.Bind(eventManager);
            
            StartCoroutine(dayNightCycle.StartCycle());
        }
    }
}