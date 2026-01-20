using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Controllers.MoveStates;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Controllers
{
    public class PlayerController
    {
        PlayerConfig _config;
        FSM<PlayerMoveStateName> _moveFSM = new ();
        
        public PlayerController(Rigidbody rigidbody)
        {
            _config = ServiceLocator.Current.Get<ConfigProviderService>().GetPlayerConfig();
            _moveFSM.AddState(new MoveState(rigidbody,  _config))
                .AddState(new IdleState(rigidbody));
        }

        public void FixedUpdate()
        {
            _moveFSM.Tick(Time.fixedDeltaTime);
        }
    }
}