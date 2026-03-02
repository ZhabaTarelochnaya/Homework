using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.States
{
    public class InitState : FSMState<GameStateName>
    {
        readonly GameState _gameState;

        public InitState(GameState gameState) : base(GameStateName.Init)
        {
            _gameState = gameState;
        }

        public override void OnEnter()
        {
            _gameState.GameStateName = GameStateName.Init;
        }

        public override GameStateName GetNextState()
        {
            var connectionsCount = NetworkServer.connections.Count;
            var playerCount = _gameState.PlayerStates.Count;
            if (connectionsCount == playerCount)
            {
                foreach (var state in _gameState.PlayerStates.Values)
                {
                    if (!state.IsInitialized) return GameStateName.Init;
                }
                return GameStateName.MatchGoing;
            }
            return GameStateName.Init;
        }
    }
}