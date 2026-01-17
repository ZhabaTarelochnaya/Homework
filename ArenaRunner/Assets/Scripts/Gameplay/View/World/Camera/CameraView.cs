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
            var pos = Vector2.Lerp(transform.position, _viewModel.Position, 0.1f);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}