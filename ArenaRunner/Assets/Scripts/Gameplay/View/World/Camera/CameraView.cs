using DefaultNamespace.Gameplay.View.Camera;
using UnityEngine;

namespace DefaultNamespace.Gameplay.View
{
    public class CameraView : MonoBehaviour
    {
        CameraViewModel _viewModel;
        public void Bind(CameraViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        void LateUpdate()
        {
            _viewModel.LateUpdate();
            var velocity = Vector2.zero;
            var pos = Vector2.SmoothDamp(transform.position, _viewModel.Position, ref velocity,0.02f);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}