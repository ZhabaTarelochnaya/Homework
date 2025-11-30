using System;
using _1.Gameplay.Data;
using _1.Gameplay.UI;
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
        [SerializeField] GameObject _gameplayUI;
        GameplayRequests _gameplayRequests;
        InputActions _inputActions;

        public void Bind(GameplayRequests gameplayRequests, UIRoot uiRoot, InputActions inputActions)
        {
            _gameplayRequests = gameplayRequests;
            _inputActions = inputActions;

            var gameplayData = new GameplayData();
            var gameplayDataProxy = new GameplayDataProxy(gameplayData);
            
            var gameplayUIInstance = Instantiate(_gameplayUI, uiRoot.transform);
            var gameplayUI = gameplayUIInstance.GetComponent<GameplayUI>();
            
            _cannon.Bind(inputActions, mainCamera, gameplayDataProxy);
            gameplayUI.Bind(gameplayRequests, gameplayDataProxy);
            
            inputActions.Disable();
            inputActions.Gameplay.Enable();
            _inputActions.Gameplay.Exit.performed += OnExit;
            _inputActions.Gameplay.Restart.performed += OnRestart;
            
            var gameManager = new GameManager(_spawnZone, gameplayDataProxy);
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