using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Controllers.HandsStates
{
    public class ReloadState : FSMState<HandsStateName>
    {
        readonly Weapon _weapon;
        readonly PlayerState _playerState;
        readonly IInputService _inputService;
        readonly PlayerConfig _playerConfig;
        float _timer;

        public ReloadState(Weapon weapon, PlayerState playerState) : base(HandsStateName.Reload)
        {
            _weapon = weapon;
            _playerState = playerState;
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
        }

        public override void OnEnter()
        {
            _weapon.CmdStartReload();
        }

        public override void OnExit()
        {
            _weapon.CmdStopReload();
        }

        public override HandsStateName GetNextState()
        {
            if (_inputService.GetHealButtonDown() 
                && _playerState.MedKitCount > 0 
                && _playerState.CurrentHp < _playerConfig.MaxHealth)
            {
                return HandsStateName.Heal;
            }
            if (_inputService.GetGrenadeButtonDown() && _playerState.GrenadeCount > 0)
            {
                return HandsStateName.ThrowGrenade;
            }
            if (_weapon.CurrentAmmo > 0 && _inputService.GetShootButton())
            {
                return HandsStateName.Shoot;
            }
            if (_weapon.CurrentAmmo == _weapon.Config.MaxAmmo)
            {
                return HandsStateName.Idle;
            }
            return HandsStateName.Reload;
        }
    }
}