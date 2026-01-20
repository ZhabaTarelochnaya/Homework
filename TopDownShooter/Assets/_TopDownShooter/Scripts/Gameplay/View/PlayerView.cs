using System;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour
    {
        PlayerController _controller;
        public Rigidbody Rigidbody { get; private set; }

        public void Bind(PlayerController playerController)
        {
            _controller = playerController;
        }
        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            _controller.FixedUpdate();
        }
    }
}