using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Config.Weapon;
using _MultiplayerFPS.Scripts.Services.Config;
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
        readonly PlayerState _playerState;
        readonly IInputService _inputService;
        readonly ICameraManager _cameraManager;
        readonly PlayerConfig _playerConfig;

        public ShootState(Weapon weapon, PlayerState playerState) : base(HandsStateName.Shoot)
        {
            _weapon = weapon;
            _playerState = playerState;
            _inputService = ServiceLocator.Current.Get<IInputService>();
            _cameraManager = ServiceLocator.Current.Get<ICameraManager>();
            _playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
        }
        
        public override void Tick(float deltaTime)
        {
            Camera cam = _cameraManager.CurrentCamera;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Vector3 targetPoint;
            
            if (Physics.Raycast(ray, out RaycastHit hit, _weapon.Config.Range, _weapon.Config.AimLayers))
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