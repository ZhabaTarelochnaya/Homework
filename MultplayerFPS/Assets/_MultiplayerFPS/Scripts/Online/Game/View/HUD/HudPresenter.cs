using _MultiplayerFPS.Scripts.Components;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Config.Weapon;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class HudPresenter : IPresenter
    {
        readonly Weapon _weapon;
        readonly PlayerState _playerState;
        readonly IHudView _view;
        readonly GameState _gameState;

        public HudPresenter(Weapon weapon, PlayerState playerState, IHudView view)
        {
            _weapon = weapon;
            _playerState = playerState;
            _view = view;
            _gameState = ServiceLocator.Current.Get<IStateService>().GameState;
            var startingWeapon = ServiceLocator.Current.Get<IConfigService>()
                .Get<PlayerConfig>()
                .StartingWeapon;
            var config = (IWeaponConfig)startingWeapon;
            _view.SetAmmo(_playerState.CurrentAmmo, config.MaxAmmo);
            var time = ServiceLocator.Current.Get<IConfigService>().Get<GameConfig>().MatchDuration;
            _view.SetTimer(time);
            Enable();
        }
        public void Enable()
        {
            if (_weapon.Config != null)
            {
                _view.SetAmmo(_playerState.CurrentAmmo, _weapon.Config.MaxAmmo);
            }
            _view.SetHealth(_playerState.CurrentHp);
            _view.UpdatingPing += ViewOnUpdatingPing;
            _playerState.CurrentHpChanged += PlayerStateOnCurrentHpChanged;
            _playerState.CurrentAmmoChanged += PlayerStateOnCurrentAmmoChanged;
            _playerState.MedKitCountChanged += PlayerStateOnMedKitCountChanged;
            _playerState.GrenadeCountChanged += PlayerStateOnGrenadeCountChanged;
            _gameState.CurrentTimeChanged += GameStateOnCurrentTimeChanged;
            _view.Enable();
        }
        public void Disable()
        {
            _view.Disable();
            _view.UpdatingPing -= ViewOnUpdatingPing;
            _playerState.CurrentHpChanged -= PlayerStateOnCurrentHpChanged;
            _playerState.CurrentAmmoChanged -= PlayerStateOnCurrentAmmoChanged;
            _playerState.MedKitCountChanged -= PlayerStateOnMedKitCountChanged;
            _playerState.GrenadeCountChanged -= PlayerStateOnGrenadeCountChanged;
            _gameState.CurrentTimeChanged -= GameStateOnCurrentTimeChanged;
        }
        void PlayerStateOnGrenadeCountChanged(int obj) => _view.SetGrenade(obj);
        void PlayerStateOnMedKitCountChanged(int obj) => _view.SetMedKit(obj);
        void PlayerStateOnCurrentAmmoChanged(int obj) => _view.SetAmmo(obj ,_weapon.Config.MaxAmmo);
        void PlayerStateOnCurrentHpChanged(int arg1, int arg2) => _view.SetHealth(arg2);
        void GameStateOnCurrentTimeChanged(int obj) => _view.SetTimer(obj);
        void ViewOnUpdatingPing()
        {
            int ping = Mathf.RoundToInt((float)(NetworkTime.rtt * 1000));
            _view.SetPing(ping);
            _view.SetPlayerCount(NetworkServer.connections.Count);
        }
    }
}