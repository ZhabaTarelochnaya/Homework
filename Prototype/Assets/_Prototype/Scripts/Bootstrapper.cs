using _Prototype.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] LoadingScreen _loadingScreen;

    void Awake()
    {
        DontDestroyOnLoad(_eventSystem.gameObject);
        var coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        DontDestroyOnLoad(coroutines.gameObject);
        DontDestroyOnLoad(_loadingScreen);
    }
}
