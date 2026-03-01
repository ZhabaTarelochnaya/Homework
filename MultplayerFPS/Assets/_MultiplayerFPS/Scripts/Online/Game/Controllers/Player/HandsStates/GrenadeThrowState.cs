using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.FiniteStateMachine;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Controllers.HandsStates
{
    public class GrenadeThrowState : FSMState<HandsStateName>
    {
        readonly PlayerState _playerState;
        readonly GameNetworkPlayer _player;
        readonly GrenadeConfig _config;
        readonly ICameraManager _cameraManager;
        float _timer;
        public GrenadeThrowState(PlayerState playerState, GameNetworkPlayer player) 
            : base(HandsStateName.ThrowGrenade)
        {
            _playerState = playerState;
            _player = player;
            _config = ServiceLocator.Current.Get<IConfigService>().Get<GrenadeConfig>();
            _cameraManager = ServiceLocator.Current.Get<ICameraManager>();
        }
        public override void OnEnter()
        {
            _timer = _config.ThrowDuration;
            _playerState.CmdChangeIsThrowingGrenade(true);
            Debug.Log("GrenadeThrowState Enter");
        }
        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
        }

        public override void OnExit()
        {
            var direction = _cameraManager.CurrentCamera.transform.forward;
            _player.CmdThrowGrenade(direction);
            _playerState.CmdChangeIsThrowingGrenade(false);
            
            Debug.Log("GrenadeThrowState Exit");
        }

        public override HandsStateName GetNextState()
        {
            if (_timer <= 0)
            {
                return HandsStateName.Idle;
            }
            return HandsStateName.ThrowGrenade;
        }
    }
}