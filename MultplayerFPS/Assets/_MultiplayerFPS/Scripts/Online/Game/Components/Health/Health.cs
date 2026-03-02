using System;
using System.Collections;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components.Health
{
    public class Health : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnCurrentHpChanged))]
        int _currentHp;
        [SyncVar(hook = nameof(OnIsDeadChanged))]
        bool _isDead;
        WaitForSeconds _waitForRespawn;
        Coroutine _respawnCoroutine;
        [field: SerializeField] public int MaxHp { get; set; }
        public int CurrentHp => _currentHp;
        public bool IsDead => _currentHp <= 0;
        
        public delegate void CurrentHpChangedHandler(int currentHp, int damage);
        public event CurrentHpChangedHandler ClientCurrentHpChanged;
        public event Action<bool> ClientIsDeadChanged;
        public event CurrentHpChangedHandler ServerCurrentHpChanged;
        public event Action<bool> ServerIsDeadChanged;
        
        public override void OnStartServer()
        {
            _currentHp = MaxHp;
            var respawnTime = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>().RespawnTime;
            _waitForRespawn = new WaitForSeconds(respawnTime);
        }
        [Server]
        public void Damage(int damage)
        {
            Debug.Log(damage);
            if (_isDead) return;
            var currentHp = _currentHp - damage;
            var newHp = Mathf.Clamp(currentHp, 0, MaxHp);
            if (newHp == _currentHp) return;
            ServerCurrentHpChanged?.Invoke(_currentHp, newHp);
            _currentHp = newHp;
            if (_currentHp == 0)
            {
                _isDead = true;
                ServerIsDeadChanged?.Invoke(_isDead);
            }
        }
        [Server]
        public void Revive()
        {
            FullHeal();
            _isDead = false;
            ServerIsDeadChanged?.Invoke(_isDead);
        }
        [Server]
        public void FullHeal()
        {
            ServerCurrentHpChanged?.Invoke(_currentHp, MaxHp);
            _currentHp = MaxHp;
        }

        [Command]
        public void CmdRevive()
        {
            if (_respawnCoroutine != null)
            {
                return;
            }
            StartCoroutine(WaitForRespawn());
        }
        IEnumerator WaitForRespawn()
        {
            yield return _waitForRespawn;
            Revive();
        }
        void OnCurrentHpChanged(int oldHp, int newHp)
        {
            ClientCurrentHpChanged?.Invoke(oldHp,newHp);
        }
        void OnIsDeadChanged(bool oldValue, bool newValue)
        {
            ClientIsDeadChanged?.Invoke(newValue);
        }
    }
}