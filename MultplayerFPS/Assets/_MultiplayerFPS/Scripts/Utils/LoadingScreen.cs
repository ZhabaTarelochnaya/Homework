using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Utils
{
    public class LoadingScreen : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}