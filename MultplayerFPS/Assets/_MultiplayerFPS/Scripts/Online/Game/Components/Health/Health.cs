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
        public bool IsDead => _isDead;
        public event Action<int,int> CurrentHpChanged;
        public event Action<bool> IsDeadChanged;
        
        public override void OnStartServer()
        {
            _currentHp = MaxHp;
            var respawnTime = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>().RespawnTime;
            _waitForRespawn = new WaitForSeconds(respawnTime);
        }
        [Server]
        public void Damage(int damage)
        {
            if (_isDead) return;
            var currentHp = _currentHp - damage;
            var newHp = Mathf.Clamp(currentHp, 0, MaxHp);
            if (newHp == _currentHp) return;
            _currentHp = newHp;
            if (_currentHp == 0)
            {
                _isDead = true;
            }
        }
        [Server]
        public void FullHeal()
        {
            _currentHp = MaxHp;
        } 
        [Command]
        public void CmdStartRevive()
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
        [Server]
        void Revive()
        {
            FullHeal();
            _isDead = false;
        }
        void OnCurrentHpChanged(int oldHp, int newHp)
        {
            CurrentHpChanged?.Invoke(oldHp,newHp);
        }
        void OnIsDeadChanged(bool oldValue, bool newValue)
        {
            IsDeadChanged?.Invoke(newValue);
        }
    }
}