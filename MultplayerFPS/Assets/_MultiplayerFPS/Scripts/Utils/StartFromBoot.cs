using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.ExceptionPopUp;
using _MultiplayerFPS.Scripts.Utils.LoadingScreen;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _MultiplayerFPS.Scripts.Game.Utils
{
    public class StartFromBoot 
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            SceneManager.LoadScene("Boot");
        }
    }
}