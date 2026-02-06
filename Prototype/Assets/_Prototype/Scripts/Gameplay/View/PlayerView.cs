using System;
using _Prototype.Scripts.Gameplay.Controllers;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour
    {
        IPlayerController _playerController;
        Rigidbody2D _rigidbody;

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