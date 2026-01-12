using System;
using UnityEngine;

namespace DefaultNamespace.Gameplay.World.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour
    {
        PlayerViewModel _viewModel;
        Rigidbody _rigidbody;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Bind(PlayerViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        void Update()
        {
            _viewModel.Direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        void FixedUpdate()
        {
            _viewModel.FixedUpdate();
            _rigidbody.velocity = _viewModel.Velocity;
            
            _viewModel.Position = _rigidbody.position;
        }
    }
}