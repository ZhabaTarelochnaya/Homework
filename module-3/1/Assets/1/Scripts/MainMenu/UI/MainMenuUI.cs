using System;
using UnityEngine;

namespace _1.MainMenu.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] MainScreen _mainScreen;
        public void Bind(MainMenuRequests requests)
        {
            _mainScreen.Bind(requests);
            requests.LoadGameplayRequested += Destroy;
        }
        void Destroy()
        {
            Destroy(gameObject);
        }
    }
}