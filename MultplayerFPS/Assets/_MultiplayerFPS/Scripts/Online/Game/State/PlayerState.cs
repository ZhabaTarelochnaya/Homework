using System;
using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.State
{
    public class PlayerState : NetworkBehaviour
    {
        [SerializeField] Health _health;
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
        [SyncVar(hook = nameof(OnIsHealingChanged)), HideInInspector]  
        public bool IsHealing;
        [SyncVar(hook = nameof(OnIsThrowingGrenadeChanged)), HideInInspector]  
        public bool IsThrowingGrenade;
        [SyncVar(hook = nameof(OnIsDeadChanged)), HideInInspector] 
        public bool IsDead;
        [SyncVar, HideInInspector]
        public Vector3 Position;
        public readonly SyncList<PickupName> Pickups = new ();
        [SyncVar(hook = nameof(OnMedKitCountChanged))]
        int _medKitCount;
        [SyncVar(hook = nameof(OnGrenadeCountChanged))]
        int _grenadeCount;
        public int MedKitCount => _medKitCount;
        public int GrenadeCount => _grenadeCount;
        public event Action<string> NicknameChanged;
        public event Action<Color> ColorChanged;
        public event Action<int, int> CurrentHealthChanged;
        public event Action<int> CurrentAmmoChanged;
        public event Action<bool> IsShootingChanged;
        public event Action<bool> IsReloadingChanged;
        public event Action<bool> IsDeadChanged;
        public event Action<bool> IsHealingChanged;
        public event Action<bool> IsThrowingGrenadeChanged;
        public event Action<int> MedKitCountChanged;
        public event Action<int> GrenadeCountChanged;

        public override void OnStartServer()
        {
            Pickups.OnChange += OnChange;
        }
        public override void OnStopServer()
        {
            Pickups.OnChange -= OnChange;
        }

        [Command]
        public void CmdChangePosition(Vector3 position) => Position = position;

        [Command]
        public void CmdUseMedKit()
        {
            var config = ServiceLocator.Current.Get<IConfigService>().Get<MedKitConfig>();
            var isRemoved = Pickups.Remove(PickupName.MedKit);
            if (!isRemoved) return;
            _health.Damage(-config.Heal);
        }
        [Command]
        public void CmdChangeIsHealing(bool value) => IsHealing = value;
        [Command]
        public void CmdChangeIsThrowingGrenade(bool value) => IsThrowingGrenade = value;

        void OnChange(SyncList<PickupName>.Operation arg1, int arg2, PickupName arg3)
        {
            if (arg1 == SyncList<PickupName>.Operation.OP_ADD)
            {
                if (arg3 == PickupName.MedKit)
                {
                    _medKitCount++;
                }
            }
            else if (arg1 == SyncList<PickupName>.Operation.OP_REMOVEAT)
            {
                if (arg3 == PickupName.MedKit)
                {
                    _medKitCount--;
                }
            }
        }
        void OnNicknameChanged(string oldNickname, string newNickname) => NicknameChanged?.Invoke(newNickname);
        void OnColorChanged(Color oldColor, Color newColor) => ColorChanged?.Invoke(newColor);
        void OnCurrentHealthChanged(int oldHealth, int newHealth) => CurrentHealthChanged?.Invoke(oldHealth, newHealth);
        void OnCurrentAmmoChanged(int oldAmmo, int newAmmo) => CurrentAmmoChanged?.Invoke(newAmmo);
        void OnIsShootingChanged(bool oldValue, bool newValue) => IsShootingChanged?.Invoke(newValue);
        void OnIsReloadingChanged(bool oldValue, bool newValue) => IsReloadingChanged?.Invoke(newValue);
        void OnIsDeadChanged(bool oldValue, bool newValue) => IsDeadChanged?.Invoke(newValue);
        void OnIsHealingChanged(bool oldValue, bool newValue) => IsHealingChanged?.Invoke(newValue);
        void OnIsThrowingGrenadeChanged(bool oldValue, bool newValue) => IsThrowingGrenadeChanged?.Invoke(newValue);
        void OnMedKitCountChanged(int oldValue, int newValue) => MedKitCountChanged?.Invoke(newValue);
        void OnGrenadeCountChanged(int oldValue, int newValue) => GrenadeCountChanged?.Invoke(newValue);
    }
}