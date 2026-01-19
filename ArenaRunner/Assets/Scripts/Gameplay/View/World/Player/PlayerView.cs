using DefaultNamespace.Gameplay.View.PickUp;
using Gameplay.View.World.Enemies.HitHurtBoxes;
using UnityEngine;

namespace DefaultNamespace.Gameplay.World.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] PickUpCollectorView pickUpCollectorView;
        [SerializeField] HurtBoxView hurtBoxView;
        PlayerViewModel _viewModel;
        Rigidbody2D _rigidbody;
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Bind(PlayerViewModel viewModel)
        {
            _viewModel = viewModel;
            pickUpCollectorView.Bind(_viewModel.PickUpCollectorViewModel);
            hurtBoxView.Bind(_viewModel.HurtBoxViewModel);
        }
        void FixedUpdate()
        {
            _viewModel.Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _viewModel.FixedUpdate();
            
            _rigidbody.velocity = _viewModel.Velocity;
            _viewModel.Position = _rigidbody.position;
        }
    }
}