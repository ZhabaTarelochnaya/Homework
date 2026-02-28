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
        
        public event Action<string> NicknameChanged;
        public event Action<Color> ColorChanged;
        public event Action<int, int> CurrentHealthChanged;
        public event Action<int> CurrentAmmoChanged;
        
        void OnNicknameChanged(string oldNickname, string newNickname) => NicknameChanged?.Invoke(newNickname);
        void OnColorChanged(Color oldColor, Color newColor) => ColorChanged?.Invoke(newColor);
        void OnCurrentHealthChanged(int oldHealth, int newHealth) => CurrentHealthChanged?.Invoke(oldHealth, newHealth);
        void OnCurrentAmmoChanged(int oldAmmo, int newAmmo) => CurrentAmmoChanged?.Invoke(newAmmo);
    }
}