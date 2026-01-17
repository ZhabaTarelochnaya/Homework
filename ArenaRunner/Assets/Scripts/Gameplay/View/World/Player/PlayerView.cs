using DefaultNamespace.Gameplay.View.PickUp;
using UnityEngine;

namespace DefaultNamespace.Gameplay.World.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] PickUpCollectorView pickUpCollectorView;
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