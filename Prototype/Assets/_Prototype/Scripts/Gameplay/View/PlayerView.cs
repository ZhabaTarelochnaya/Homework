using System;
using _Prototype.Scripts.Gameplay.Controllers;
using _Prototype.Scripts.Gameplay.Services;
using _Prototype.Scripts.Gameplay.View.HurtBox;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IPositionUser
    {
        IPlayerController _playerController;
        Rigidbody2D _rigidbody;
        [field: SerializeField] public HurtBoxView HurtBoxView { get; private set; }
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