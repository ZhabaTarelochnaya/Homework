using System;
using _MultiplayerFPS.Scripts.Services.CoroutineRunner;
using _MultiplayerFPS.Scripts.Services.LoggerService;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.States
{
    public class MatchEndedState : FSMState<GameStateName>
    {
        readonly GameState _gameState;
        readonly Action _enableCursor;

        public MatchEndedState(GameState gameState, Action enableCursor) : base(GameStateName.MatchEnded)
        {
            _gameState = gameState;
            _enableCursor = enableCursor;
        }

        public override void OnEnter()
        {
            _gameState.GameStateName = GameStateName.MatchEnded;
            ServiceLocator.Current.Get<ILoggerService>().Log("Match ended");
            ServiceLocator.Current.Get<ICoroutineRunnerService>().StopAllCoroutines();
            _enableCursor?.Invoke();
        }

        public override GameStateName GetNextState() => GameStateName.MatchEnded;
    }
}