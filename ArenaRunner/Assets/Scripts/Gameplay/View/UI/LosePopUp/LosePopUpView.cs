using UnityEngine;

namespace DefaultNamespace.LosePopUp
{
    public class LosePopUpView : MonoBehaviour
    {
        LosePopUpViewModel _viewModel;
        public void Bind(LosePopUpViewModel viewModel)
        {
            _viewModel = viewModel;
            viewModel.UIInstance = gameObject;
        }

        public void OnRestartButtonPressed() => _viewModel.ReloadGameplay();
    }
}