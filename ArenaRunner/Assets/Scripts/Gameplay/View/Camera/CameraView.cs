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
            var pos = _viewModel.Position;
            transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
            transform.rotation = _viewModel.CameraRotation;
        }
    }
}