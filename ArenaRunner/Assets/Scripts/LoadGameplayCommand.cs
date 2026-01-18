
using System.Collections;
using DefaultNamespace.Gameplay;
using DefaultNamespace.Gameplay.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace DefaultNamespace
{
    public class LoadGameplayCommand
    {
        readonly Coroutines _coroutines;
        readonly UIRoot _uiRoot;
        readonly GameState _gameState;
        readonly GameConfig _gameConfig;

        public LoadGameplayCommand(Coroutines coroutines, UIRoot uiRoot, 
            GameState gameState, GameConfig gameConfig)
        {
            _coroutines = coroutines;
            _uiRoot = uiRoot;
            _gameState = gameState;
            _gameConfig = gameConfig;
        }
        IEnumerator LoadAndStartGameplay()
        {
            _uiRoot.ShowLoadingScreen();
            _gameState.GameStateName = GameStateName.Init;
            yield return LoadScene(SceneNames.Boot);
            yield return LoadScene(SceneNames.Gameplay);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Bind(_uiRoot, _gameConfig, _gameState, this);
            _uiRoot.HideLoadingScreen();
        }
        IEnumerator LoadScene(SceneNames sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName.ToString());
        }
        public void Execute()
        {
            _coroutines.StartCoroutine(LoadAndStartGameplay());
        }
    }
}