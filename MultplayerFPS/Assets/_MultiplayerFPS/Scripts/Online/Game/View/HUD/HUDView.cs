using System;
using System.Collections;
using _MultiplayerFPS.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class HUDView : MonoBehaviour, IView
    {
        WaitForSeconds _waitForSeconds = new (0.25f);
        Coroutine _updateCoroutine;
        [SerializeField] TMP_Text _pingText;
        [SerializeField] TMP_Text _playerCountText;
        
        public event Action UpdatingPing;
        
        public void SetPingUpdateInterval(float pingUpdateInterval)
        {
            _waitForSeconds = new WaitForSeconds(pingUpdateInterval);
        }
        public void SetPing(int ping) => _pingText.text = $"Ping: {ping} ms";
        public void SetPlayerCount(int playerCount) => _playerCountText.text = $"Players: {playerCount}";
        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);
        
        void OnEnable() => _updateCoroutine = StartCoroutine(UpdateUI());
        void OnDisable() => StopCoroutine(_updateCoroutine);

        IEnumerator UpdateUI()
        {
            while (true)
            {
                UpdatingPing?.Invoke();
                yield return _waitForSeconds;
            }
        }
    }
}