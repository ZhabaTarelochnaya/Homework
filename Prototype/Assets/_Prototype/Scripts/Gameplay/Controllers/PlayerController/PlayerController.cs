using System;
using _Prototype.Scripts.Gameplay.Controllers.HitBoxController;
using _Prototype.Scripts.Gameplay.Services.HealthService;
using _Prototype.Scripts.Gameplay.Services.InputService;
using _Prototype.Scripts.Gameplay.Services.MoveService;
using _Prototype.Scripts.Gameplay.View.HurtBox;
using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Controllers
{
    public class PlayerController : IPlayerController, IHurtBoxController
    {
        readonly IMoveService _moveService = ServiceLocator.Current.Get<IMoveService>();
        readonly IInputService _inputService = ServiceLocator.Current.Get<IInputService>();
        readonly IHealthService _healthService = ServiceLocator.Current.Get<IHealthService>();
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        
        float _speed = 10;

        public PlayerController(IHurtBoxView hurtBoxView)
        {
            hurtBoxView.Hurt += HurtBoxViewHurt;
        }
        void HurtBoxViewHurt(int damage) => _healthService.Damage(this, damage);

        public void FixedUpdate(Rigidbody2D rigidbody)
        {
            _moveService.Move(rigidbody, _inputService.GetMovementInput(), _speed);
        }
    }
}