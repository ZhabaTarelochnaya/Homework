using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.ServerCommands;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Controllers.HandsStates
{
    public class HealState : FSMState<HandsStateName>
    {
        readonly PlayerState _playerState;
        readonly MedKitConfig _config;
        readonly IPlayerCommandsService _playerCommandsService;
        float _timer;
        public HealState() : base(HandsStateName.Heal)
        {
            _config = ServiceLocator.Current.Get<IConfigService>().Get<MedKitConfig>();
            _playerCommandsService = ServiceLocator.Current.Get<IPlayerCommandsService>();
        }
        public override void OnEnter()
        {
            _timer = _config.HealDuration;
            _playerCommandsService.CmdChangeIsHealing(true);
        }
        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
        }
        public override void OnExit()
        {
            _playerCommandsService.CmdUseMedKit();
            _playerCommandsService.CmdChangeIsHealing(false);
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