using System;
using TestPlatformer.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _1.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] SpawnZone _spawnZone;
        GameplayRequests _gameplayRequests;
        InputActions _inputActions;
        public void Bind(GameplayRequests gameplayRequests, UIRoot uiRoot, InputActions inputActions)
        {
            _gameplayRequests = gameplayRequests;
            _inputActions = inputActions;
            
            inputActions.Disable();
            inputActions.Gameplay.Enable();

            _inputActions.Gameplay.Exit.performed += OnExit;
            
            var gameManager = new GameManager(_spawnZone);
            StartCoroutine(gameManager.Run());
        }

        void OnExit(InputAction.CallbackContext obj)
        {
            _gameplayRequests.LoadMainMenuRequested.Invoke();
        }

        void OnDestroy()
        {
            _inputActions.Gameplay.Exit.performed -= OnExit;
        }
    }
}