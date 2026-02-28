using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Controllers.HandsStates
{
    public class ShootState : FSMState<HandsStateName>
    {
        readonly Weapon _weapon;
        readonly IInputService _inputService;
        readonly ICameraManager _cameraManager;

        public ShootState(Weapon weapon) : base(HandsStateName.Shoot)
        {
            _weapon = weapon;
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _cameraManager = ServiceLocator.Current.Get<ICameraManager>();
        }
        
        public override void Tick(float deltaTime)
        {
            Camera cam = _cameraManager.CurrentCamera;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out RaycastHit hit, _weapon.Config.Range))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.origin + ray.direction * _weapon.Config.Range;
            }
            Vector3 shootOrigin = _weapon.ShootOrigin.position;
            Vector3 shootDirection = (targetPoint - shootOrigin).normalized;
                
            _weapon.CmdShoot(shootOrigin, shootDirection);
        }

        public override void OnExit()
        {
            _weapon.CmdStopShoot();
        }

        public override HandsStateName GetNextState()
        {
            if (_weapon.CurrentAmmo <= 0)
            {
                return HandsStateName.Reload;
            }
            if (!_inputService.GetShootButton())
            {
                return HandsStateName.Idle;
            }
            return HandsStateName.Shoot;
        }
    }
}