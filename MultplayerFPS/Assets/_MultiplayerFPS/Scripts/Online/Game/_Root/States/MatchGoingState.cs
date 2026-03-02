using _MultiplayerFPS.Scripts.Services.LoggerService;
using _MultiplayerFPS.Scripts.Services.MatchTime;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;

namespace _MultiplayerFPS.Scripts.States
{
    public class MatchGoingState : FSMState<GameStateName>
    {
        readonly GameState _gameState;
        readonly IMatchTimeService _matchTimeService;
        GameStateName nextState;

        public MatchGoingState(GameState gameState, IMatchTimeService matchTimeService) 
            : base(GameStateName.MatchGoing)
        {
            _gameState = gameState;
            _matchTimeService = matchTimeService;
        }
        public override void OnEnter()
        {
            _gameState.GameStateName = GameStateName.MatchGoing;
            nextState = GameStateName.MatchGoing;
            _matchTimeService.TimeOut += MatchTimeServiceOnTimeOut;
            _matchTimeService.StartTimer();
            ServiceLocator.Current.Get<ILoggerService>().Log("Match started");
        }
        public override void OnExit()
        {
            _matchTimeService.TimeOut -= MatchTimeServiceOnTimeOut;
        }
        void MatchTimeServiceOnTimeOut()
        {
            nextState = GameStateName.MatchEnded;
        }

        public override GameStateName GetNextState()
        {
            return nextState;
        }
    }
}