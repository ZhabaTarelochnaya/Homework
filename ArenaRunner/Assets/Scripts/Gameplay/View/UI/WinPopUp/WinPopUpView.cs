using UnityEngine;

namespace DefaultNamespace.WinPopUp
{
    public class WinPopUpView : MonoBehaviour
    {
        WinPopUpViewModel _viewModel;
        
        public void Bind(WinPopUpViewModel viewModel)
        {
            _viewModel = viewModel;
            viewModel.UIInstance = gameObject;
        }

        public void OnRestartButtonPressed() => _viewModel.ReloadGameplay();
    }
}