using System;
using _TopDownShooter.Scripts;
using _TopDownShooter.Scripts.Controllers;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.View;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    bool _isBound;
    CameraManager _cameraManager;
    [SerializeField] PlayerView _playerView;

    public void Bind(UIRoot uiRoot)
    {
        GameplayServiceRegistrations.Register(_playerView.transform);
        BindPlayer();
        
        _isBound = true;
        
        _cameraManager = ServiceLocator.Current.Get<CameraManager>();
        _cameraManager.SetTarget(_playerView.transform);
        var weaponManager = ServiceLocator.Current.Get<WeaponManager>();
        weaponManager.Equip(WeaponName.Pistol);
    }

    void BindPlayer()
    {
        var playerController = new PlayerController(_playerView.Rigidbody, _playerView.HurtBox);
        _playerView.Bind(playerController);
    }

    void LateUpdate()
    {
        _cameraManager.FollowTarget(_playerView.transform);
    }

    void OnDestroy()
    {
        if (!_isBound) return;
        GameplayServiceRegistrations.Unregister();
    }
}
