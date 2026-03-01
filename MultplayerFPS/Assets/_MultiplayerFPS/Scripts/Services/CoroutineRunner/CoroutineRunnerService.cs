using System.Collections;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.CoroutineRunner
{
    public class CoroutineRunnerService : ICoroutineRunnerService
    {
        Coroutines _coroutines;
        public CoroutineRunnerService()
        {
            var gameObject = new GameObject("CoroutineRunnerService");
            _coroutines = gameObject.AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines);
        }
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _coroutines.StartCoroutine(routine);
        }
        public void StopCoroutine(Coroutine coroutine)
        {
            _coroutines.StopCoroutine(coroutine);
        }
        class Coroutines : MonoBehaviour { }
    }
}