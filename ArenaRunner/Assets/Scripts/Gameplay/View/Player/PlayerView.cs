using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Gameplay.World.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour
    {
        PlayerViewModel _viewModel;
        Rigidbody _rigidbody;
        float xRotation = -90;
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Bind(PlayerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        void LateUpdate()
        {
            var mouseX = Input.GetAxis("Mouse X") * _viewModel.MouseSensitivity;
            var mouseY = Input.GetAxis("Mouse Y") * _viewModel.MouseSensitivity;
            xRotation -= mouseY;
            xRotation = Math.Clamp(xRotation, -90f, 90f);
            _viewModel.CameraRotation = Quaternion.Euler(xRotation, _viewModel.CameraRotation.eulerAngles.y + mouseX, 0.0f);
        }
        void FixedUpdate()
        {
            _viewModel.Direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _viewModel.FixedUpdate();
            
            _rigidbody.velocity = _viewModel.Velocity;
            _viewModel.Position = _rigidbody.position;
        }
    }
}