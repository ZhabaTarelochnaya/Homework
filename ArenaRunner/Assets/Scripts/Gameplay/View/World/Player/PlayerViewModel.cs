using System;
using DefaultNamespace.Gameplay.Data;
using DefaultNamespace.Gameplay.View.PickUp;
using Gameplay.View.World.Enemies.HitHurtBoxes;
using UnityEngine;

namespace DefaultNamespace.Gameplay.World.Player
{
    public class PlayerViewModel
    {
        readonly PlayerState _playerState;
        readonly PlayerController _playerController;
        public PickUpCollectorViewModel PickUpCollectorViewModel { get; }
        public Vector2 Velocity { get => _playerState.Velocity; set => _playerState.Velocity = value; }
        public Vector2 Position { get => _playerState.Position; set => _playerState.Position = value; }
        public Vector2 Direction { get => _playerState.InputMoveDirection; set => _playerState.InputMoveDirection = value; }
        public HurtBoxViewModel HurtBoxViewModel { get; }

        public PlayerViewModel(PlayerState playerState, PlayerController playerController)
        {
            _playerState = playerState;
            _playerController = playerController;
            PickUpCollectorViewModel = new PickUpCollectorViewModel();
            HurtBoxViewModel = new HurtBoxViewModel(playerState);
        }
        public void FixedUpdate() => _playerController.FixedUpdate();
    }
}