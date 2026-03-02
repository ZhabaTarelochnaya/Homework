using System;
using System.Collections;
using _MultiplayerFPS.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class HudView : MonoBehaviour, IHudView
    {
        WaitForSeconds _waitForSeconds = new (0.25f);
        Coroutine _updateCoroutine;
        HudPresenter _hudPresenter;
        [SerializeField] TMP_Text _pingText;
        [SerializeField] TMP_Text _playerCountText;
        [SerializeField] TMP_Text _ammoText;
        [SerializeField] TMP_Text _healthText;
        [SerializeField] TMP_Text _medKitText;
        [SerializeField] TMP_Text _grenadeText;
        [SerializeField] TMP_Text _timerText;
        
        public event Action UpdatingPing;
        
        public void SetPingUpdateInterval(float pingUpdateInterval)
        {
            _waitForSeconds = new WaitForSeconds(pingUpdateInterval);
        }
        public void SetPing(int ping) => _pingText.text = $"Ping: {ping} ms";
        public void SetPlayerCount(int playerCount) => _playerCountText.text = $"Players: {playerCount}";
        public void SetAmmo(int current, int max) => _ammoText.text = $"{current}/{max}";
        public void SetHealth(int health) => _healthText.text = $"{health}";
        public void SetMedKit(int count) => _medKitText.text = $"{count}";
        public void SetGrenade(int count) => _grenadeText.text = $"{count}";
        public void SetTimer(int time)
        {
            var minutes =  time / 60;
            var seconds = time % 60;
            string secondsString = seconds < 10 ? "0" + seconds : seconds.ToString();
            _timerText.text = $"{minutes}:{secondsString}";
        }

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