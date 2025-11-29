using System;
using TestPlatformer.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _1.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] SpawnZone _spawnZone;
        [SerializeField] CannonController _cannon;
        GameplayRequests _gameplayRequests;
        InputActions _inputActions;

        void Awake()
        {
            Debug.Log("Awake");
        }

        public void Bind(GameplayRequests gameplayRequests, UIRoot uiRoot, InputActions inputActions)
        {
            _gameplayRequests = gameplayRequests;
            _inputActions = inputActions;

            _cannon.Bind(inputActions, mainCamera);
            
            inputActions.Disable();
            inputActions.Gameplay.Enable();

            _inputActions.Gameplay.Exit.performed += OnExit;
            _inputActions.Gameplay.Restart.performed += OnRestart;
            
            var gameManager = new GameManager(_spawnZone);
            StartCoroutine(gameManager.Run());
        }

        void OnExit(InputAction.CallbackContext obj)
        {
            _gameplayRequests.LoadMainMenuRequested.Invoke();
        }
        void OnRestart(InputAction.CallbackContext obj)
        {
            _gameplayRequests.ReloadGameplayRequested.Invoke();
        }
        void OnDestroy()
        {
            if (_inputActions == null) return;
            _inputActions.Gameplay.Exit.performed -= OnExit;
            _inputActions.Gameplay.Restart.performed -= OnRestart;
        }
    }
}