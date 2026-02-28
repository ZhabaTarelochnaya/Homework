using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;

namespace _MultiplayerFPS.Scripts.Controllers.HandsStates
{
    public class IdleState : FSMState<HandsStateName>
    {
        readonly Weapon _weapon;
        readonly IInputService _inputService;
        readonly IStateService _stateService;
        
        public IdleState(Weapon weapon) : base(HandsStateName.Idle)
        {
            _weapon = weapon;
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _stateService = ServiceLocator.Current.Get<IStateService>();
        }

        public override HandsStateName GetNextState()
        {
            if (_weapon.CurrentAmmo > 0 && _inputService.GetShootButton())
            {
                return HandsStateName.Shoot;
            }
            if (_weapon.CurrentAmmo < _weapon.Config.MaxAmmo)
            {
                return HandsStateName.Reload;
            }
            return HandsStateName.Idle;
        }
    }
}