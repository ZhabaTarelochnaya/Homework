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
            
            transform.position = _viewModel.Position;
            transform.rotation = Quaternion.Euler(_viewModel.CameraRotation);
        }
    }
}