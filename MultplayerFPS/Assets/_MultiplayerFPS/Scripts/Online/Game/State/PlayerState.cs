using System;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config.Pickup;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.State
{
    public class PlayerState : NetworkBehaviour
    {
        public readonly SyncList<PickupName> Pickups = new ();
        
        [SyncVar(hook = nameof(OnIsInitializedChanged)), HideInInspector] 
        public bool IsInitialized;
        [SyncVar(hook = nameof(OnNicknameChanged)), HideInInspector] 
        public string Nickname;
        [SyncVar(hook = nameof(OnColorChanged)), HideInInspector]  
        public Color Color;
        [SyncVar(hook = nameof(OnCurrentAmmoChanged)), HideInInspector]  
        public int CurrentAmmo;
        [SyncVar(hook = nameof(OnIsShootingChanged)), HideInInspector]  
        public bool IsShooting;
        [SyncVar(hook = nameof(OnIsReloadingChanged)), HideInInspector]  
        public bool IsReloading;
        [SyncVar(hook = nameof(OnIsHealingChanged)), HideInInspector]  
        public bool IsHealing;
        [SyncVar(hook = nameof(OnIsThrowingGrenadeChanged)), HideInInspector]  
        public bool IsThrowingGrenade;
        [SyncVar(hook = nameof(OnMedKitCountChanged)), HideInInspector]
        public int MedKitCount;
        [SyncVar(hook = nameof(OnGrenadeCountChanged)), HideInInspector]
        public int GrenadeCount;
        [SyncVar, HideInInspector] 
        public float Speed;
        [SyncVar, HideInInspector] 
        public float JumpHeight;
        
        [SerializeField] Health _health;
        
        public int CurrentHp => _health.CurrentHp;
        public bool IsDead => _health.IsDead;
        public Vector3 Position => transform.position;
        
        public event Action<bool> IsInitializedChanged; 
        public event Action<string> NicknameChanged;
        public event Action<Color> ColorChanged;
        public event Action<int, int> CurrentHpChanged
        {
            add => _health.CurrentHpChanged += value;
            remove => _health.CurrentHpChanged -= value;
        }

        public event Action<PlayerState, bool> IsDeadChanged;
        public event Action<int> CurrentAmmoChanged;
        public event Action<bool> IsShootingChanged;
        public event Action<bool> IsReloadingChanged;
        public event Action<bool> IsHealingChanged;
        public event Action<bool> IsThrowingGrenadeChanged;
        public event Action<int> MedKitCountChanged;
        public event Action<int> GrenadeCountChanged;
        
        [Server]
        public void Damage(int damage) => _health.Damage(damage);
        
        public override void OnStartServer() => _health.IsDeadChanged += OnHealthIsDeadChanged;
        public override void OnStopServer() => _health.IsDeadChanged -= OnHealthIsDeadChanged;
        public override void OnStartClient() => _health.IsDeadChanged += OnHealthIsDeadChanged;
        public override void OnStopClient() => _health.IsDeadChanged -= OnHealthIsDeadChanged;
        void OnHealthIsDeadChanged(bool obj) => IsDeadChanged?.Invoke(this, obj);

        # region HOOKS
        void OnIsInitializedChanged(bool oldValue, bool newValue) => IsInitializedChanged?.Invoke(newValue);
        void OnNicknameChanged(string oldNickname, string newNickname) => NicknameChanged?.Invoke(newNickname);
        void OnColorChanged(Color oldColor, Color newColor) => ColorChanged?.Invoke(newColor);
        void OnCurrentAmmoChanged(int oldAmmo, int newAmmo) => CurrentAmmoChanged?.Invoke(newAmmo);
        void OnIsShootingChanged(bool oldValue, bool newValue) => IsShootingChanged?.Invoke(newValue);
        void OnIsReloadingChanged(bool oldValue, bool newValue) => IsReloadingChanged?.Invoke(newValue);
        void OnIsHealingChanged(bool oldValue, bool newValue) => IsHealingChanged?.Invoke(newValue);
        void OnIsThrowingGrenadeChanged(bool oldValue, bool newValue) => IsThrowingGrenadeChanged?.Invoke(newValue);
        void OnMedKitCountChanged(int oldValue, int newValue) => MedKitCountChanged?.Invoke(newValue);
        void OnGrenadeCountChanged(int oldValue, int newValue) => GrenadeCountChanged?.Invoke(newValue);
        #endregion
    }
}