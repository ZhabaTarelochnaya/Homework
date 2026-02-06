using System;
using _Prototype.Scripts.Gameplay.Controllers;
using _Prototype.Scripts.Gameplay.Services;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IPositionUser
    {
        IPlayerController _playerController;
        Rigidbody2D _rigidbody;
        public Vector2 Position 
        { 
            get => _rigidbody.position;
            set => _rigidbody.position = value;
        }

        public void Init(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _playerController.FixedUpdate(_rigidbody);
        }

    }
}