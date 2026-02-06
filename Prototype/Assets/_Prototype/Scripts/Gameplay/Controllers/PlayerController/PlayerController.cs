using System;
using _Prototype.Scripts.Gameplay.Services.InputService;
using _Prototype.Scripts.Gameplay.Services.MoveService;
using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Controllers
{
    public class PlayerController : IPlayerController
    {
        IMoveService _moveService = ServiceLocator.Current.Get<IMoveService>();
        IInputService _inputService = ServiceLocator.Current.Get<IInputService>();
        float _speed = 10;

        public void FixedUpdate(Rigidbody2D rigidbody)
        {
            _moveService.Move(rigidbody, _inputService.GetMovementInput(), _speed);
        }
    }
}