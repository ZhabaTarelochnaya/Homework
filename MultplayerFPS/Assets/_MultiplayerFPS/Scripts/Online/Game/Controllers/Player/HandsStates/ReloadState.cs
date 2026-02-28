using _MultiplayerFPS.Scripts.Components;
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
        readonly IInputService _inputService;
        float _timer;

        public ReloadState(Weapon weapon) : base(HandsStateName.Reload)
        {
            _weapon = weapon;
            _inputService = ServiceLocator.Current.Get<IInputService>();
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