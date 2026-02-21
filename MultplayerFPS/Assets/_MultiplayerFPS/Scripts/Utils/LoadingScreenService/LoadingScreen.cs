using _MultiplayerFPS.Scripts.Utils.LoadingScreenService;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Utils
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreenService
    {
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}