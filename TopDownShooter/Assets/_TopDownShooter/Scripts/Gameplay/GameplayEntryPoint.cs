using System;
using _TopDownShooter.Scripts;
using _TopDownShooter.Scripts.Controllers;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Configs.Enemies;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.GameplayStates;
using _TopDownShooter.Scripts.Utils.EventBus;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using _TopDownShooter.Scripts.Utils.StateMachine;
using _TopDownShooter.Scripts.View;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    bool _isBound;
    CameraManager _cameraManager;
    [SerializeField] PlayerView _playerView;
    [SerializeField] EnemySpawnerView _enemySpawnerView;

    public void Bind()
    {
        GameplayServiceRegistrations.Register(_playerView, _enemySpawnerView);
        
        var gameplayManager = ServiceLocator.Current.Get<GameplayStateManager>();
        gameplayManager.Initialize();
        
        _cameraManager = ServiceLocator.Current.Get<CameraManager>();
        _isBound = true;
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
