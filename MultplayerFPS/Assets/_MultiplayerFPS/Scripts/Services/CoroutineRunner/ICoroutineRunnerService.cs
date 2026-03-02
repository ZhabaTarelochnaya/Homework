using System.Collections;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.CoroutineRunner
{
    public interface ICoroutineRunnerService : IService
    {
        public Coroutine StartCoroutine(IEnumerator routine);

        public void StopCoroutine(Coroutine coroutine);
        public void StopAllCoroutines();
    }
}