using UnityEngine;

namespace DefaultNamespace
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] RectTransform _loadingScreen;
        public void ShowLoadingScreen() => _loadingScreen.gameObject.SetActive(true);
        public void HideLoadingScreen() => _loadingScreen.gameObject.SetActive(false);
    }
}