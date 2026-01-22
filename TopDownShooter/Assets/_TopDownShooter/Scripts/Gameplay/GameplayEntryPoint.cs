using System;
using _TopDownShooter.Scripts;
using _TopDownShooter.Scripts.Controllers;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Configs.Enemies;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.View;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    bool _isBound;
    CameraManager _cameraManager;
    [SerializeField] PlayerView _playerView;
    [SerializeField] EnemySpawnerView _enemySpawnerView;

    public void Bind(UIRoot uiRoot)
    {
        GameplayServiceRegistrations.Register(_playerView.transform);
        BindPlayer();
        BindEnemySpawner();
        _isBound = true;
        
        _cameraManager = ServiceLocator.Current.Get<CameraManager>();
        _cameraManager.SetTarget(_playerView.transform);
        var weaponManager = ServiceLocator.Current.Get<WeaponManager>();
        weaponManager.Equip(WeaponName.AssaultRifle);
    }

    void BindPlayer()
    {
        var playerController = new PlayerController(_playerView.Rigidbody, _playerView.HurtBox);
        _playerView.Bind(playerController);
    }
    void BindEnemySpawner()
    {
        var enemyController = new EnemySpawnerController(_enemySpawnerView.SpawnPoints, 
            _enemySpawnerView.EnemiesParent, _playerView.transform);
        _enemySpawnerView.Bind(enemyController);
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
