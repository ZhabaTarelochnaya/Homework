using System;
using System.Collections;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.CoroutineRunner;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.MatchTime
{
    public class MatchTimeService : IMatchTimeService
    {
        readonly GameState _gameState;
        readonly WaitForSeconds _waitASecond = new (1f);
        readonly ICoroutineRunnerService _coroutineRunner;

        public event Action TimeOut;

        public MatchTimeService(GameState gameState)
        {
            _gameState = gameState;
            _coroutineRunner = ServiceLocator.Current.Get<ICoroutineRunnerService>();
            _gameState.CurrentTime = ServiceLocator.Current.Get<IConfigService>().Get<GameConfig>().MatchDuration;
        }

        public void StartTimer()
        {
            _coroutineRunner.StartCoroutine(CountTime());
        }

        IEnumerator CountTime()
        {
            while (_gameState.CurrentTime >= 0)
            {
                yield return _waitASecond;
                _gameState.CurrentTime -= 1;
            }
            TimeOut?.Invoke();
        }
        
    }
}