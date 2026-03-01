using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Controllers.HandsStates
{
    public class HealState : FSMState<HandsStateName>
    {
        readonly PlayerState _playerState;
        readonly MedKitConfig _config;
        float _timer;
        public HealState(PlayerState playerState) : base(HandsStateName.Heal)
        {
            _playerState = playerState;
            _config = ServiceLocator.Current.Get<IConfigService>().Get<MedKitConfig>();
        }
        public override void OnEnter()
        {
            _timer = _config.HealDuration;
            _playerState.CmdChangeIsHealing(true);
        }
        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
        }

        public override void OnExit()
        {
            _playerState.CmdUseMedKit();
            _playerState.CmdChangeIsHealing(false);
        }

        public override HandsStateName GetNextState()
        {
            if (_timer <= 0)
            {
                return HandsStateName.Idle;
            }
            return HandsStateName.Heal;
        }
    }
}