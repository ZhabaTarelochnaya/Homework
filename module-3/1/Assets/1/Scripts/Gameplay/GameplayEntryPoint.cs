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
        GameplayData _gameplayData;
        GameplayData _record;

        public void Bind(GameplayRequests gameplayRequests, UIRoot uiRoot, InputActions inputActions)
        {
            _gameplayRequests = gameplayRequests;
            _inputActions = inputActions;
            
            _record = LoadOrCreateData();
            _gameplayData = new GameplayData();
            var gameplayDataProxy = new GameplayDataProxy(_gameplayData);
            
            var gameplayUIInstance = Instantiate(_gameplayUI, uiRoot.transform);
            var gameplayUI = gameplayUIInstance.GetComponent<GameplayUI>();
            
            _cannon.Bind(inputActions, gameplayDataProxy);
            gameplayUI.Bind(gameplayRequests, gameplayDataProxy, _record.TargetsHit);
            
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
            SaveIfRecord();
        }

        void SaveIfRecord()
        {
            if (_gameplayData.TargetsHit > _record.TargetsHit)
            {
                Debug.Log("Saving new record");
                PlayerPrefs.SetString("GameplayData", JsonUtility.ToJson(_gameplayData));
            }
        }
        GameplayData LoadOrCreateData()
        {
            var gameplayDataJson = PlayerPrefs.GetString("GameplayData");
            if (string.IsNullOrEmpty(gameplayDataJson))
            {
                return new GameplayData();
            }
            else
            {
                return JsonUtility.FromJson<GameplayData>(gameplayDataJson);
            }
        }
    }
}