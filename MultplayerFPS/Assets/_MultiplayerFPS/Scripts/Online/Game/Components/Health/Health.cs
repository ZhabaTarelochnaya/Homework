using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components.Health
{
    public class Health : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnCurrentHpChanged))]
        int _currentHp;
        public int CurrentHp => _currentHp;
        [field: SerializeField] public int MaxHp { get; set; }
        
        public delegate void CurrentHpChangedHandler(int currentHp, int damage);
        public event CurrentHpChangedHandler ClientCurrentHpChanged;
        public override void OnStartServer()
        {
            _currentHp = MaxHp;
        }

        [Server]
        public void Damage(int damage)
        {
            var currentHp = _currentHp - damage;
            _currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
            Debug.Log(_currentHp);
        }
        void OnCurrentHpChanged(int oldHp, int newHp)
        {
            ClientCurrentHpChanged?.Invoke(_currentHp,oldHp - newHp);
        }
    }
}