using UnityEngine;

namespace _MultiplayerFPS.Scripts.Offline
{
    public class SelectLobbyView : MonoBehaviour
    {
        [SerializeField] GameObject _mainMenu;

        public void OnExitButtonClick()
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}