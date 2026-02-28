using System;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.State
{
    public class PlayerState : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnNicknameChanged)), HideInInspector] 
        public string Nickname;
        [SyncVar(hook = nameof(OnColorChanged)), HideInInspector]  
        public Color Color;
        [SyncVar(hook = nameof(OnCurrentHealthChanged)), HideInInspector]  
        public int CurrentHealth;
        [SyncVar(hook = nameof(OnCurrentAmmoChanged)), HideInInspector]  
        public int CurrentAmmo;
        [SyncVar(hook = nameof(OnIsShootingChanged)), HideInInspector]  
        public bool IsShooting;
        [SyncVar(hook = nameof(OnIsReloadingChanged)), HideInInspector]  
        public bool IsReloading;
        [SyncVar(hook = nameof(OnIsDeadChanged)), HideInInspector] 
        public bool IsDead;
        
        public event Action<string> NicknameChanged;
        public event Action<Color> ColorChanged;
        public event Action<int, int> CurrentHealthChanged;
        public event Action<int> CurrentAmmoChanged;
        public event Action<bool> IsShootingChanged;
        public event Action<bool> IsReloadingChanged;
        public event Action<bool> IsDeadChanged;

        void OnNicknameChanged(string oldNickname, string newNickname) => NicknameChanged?.Invoke(newNickname);
        void OnColorChanged(Color oldColor, Color newColor) => ColorChanged?.Invoke(newColor);
        void OnCurrentHealthChanged(int oldHealth, int newHealth) => CurrentHealthChanged?.Invoke(oldHealth, newHealth);
        void OnCurrentAmmoChanged(int oldAmmo, int newAmmo) => CurrentAmmoChanged?.Invoke(newAmmo);
        void OnIsShootingChanged(bool oldValue, bool newValue) => IsShootingChanged?.Invoke(newValue);
        void OnIsReloadingChanged(bool oldValue, bool newValue) => IsReloadingChanged?.Invoke(newValue);
        void OnIsDeadChanged(bool oldValue, bool newValue) => IsDeadChanged?.Invoke(newValue);
    }
}