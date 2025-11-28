using UnityEngine;

namespace _1.MainMenu.UI
{
    public class MainScreen : MonoBehaviour
    {
        MainMenuRequests _requests;

        public void Bind(MainMenuRequests mainMenuRequests)
        {
            _requests = mainMenuRequests;
        }

        public void OnExitPressed() => _requests.ExitRequested?.Invoke();
        public void OnPlayPressed()
        {
            var num = _requests.LoadGameplayRequested.GetInvocationList();
            _requests.LoadGameplayRequested?.Invoke();
        }
    }
}