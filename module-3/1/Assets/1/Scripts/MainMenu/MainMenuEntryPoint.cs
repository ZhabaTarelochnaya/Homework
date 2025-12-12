using _1.MainMenu.UI;
using TestPlatformer.Scripts;
using UnityEngine;

namespace _1.MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] GameObject _mainMenuUIPrefab;
        public void Bind(MainMenuRequests requests, UIRoot uiRoot, InputActions inputActions)
        {
            inputActions.Disable();
            inputActions.UI.Enable();
            
            var mainMenuUIInstance = Instantiate(_mainMenuUIPrefab, uiRoot.transform);
            var mainMenuUI = mainMenuUIInstance.GetComponent<MainMenuUI>();
            mainMenuUI.Bind(requests);
        }
    }
}