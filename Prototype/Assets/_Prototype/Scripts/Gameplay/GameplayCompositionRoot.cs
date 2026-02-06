using System;
using _Prototype.Scripts.Gameplay.Controllers;
using _Prototype.Scripts.Gameplay.Services;
using _Prototype.Scripts.Gameplay.Services.InputService;
using _Prototype.Scripts.Gameplay.Services.MoveService;
using _Prototype.Scripts.Gameplay.View;
using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameplayCompositionRoot : MonoBehaviour
{
    ICameraService _cameraService;
    [SerializeField] PlayerView _playerView;

    public void Awake()
    {
        RegisterServices();
        
        IPlayerController playerController = new PlayerController();
        _playerView.Init(playerController);

        _cameraService.CurrentTarget = _playerView;
    }

    void RegisterServices()
    {
        IInputService inputService = new KeyboardInputService();
        ServiceLocator.Current.Register(inputService);
        IMoveService moveService = new MoveService();
        ServiceLocator.Current.Register(moveService);
        _cameraService = new CameraService();
        ServiceLocator.Current.Register(_cameraService);
    }
    void UnregisterServices()
    {
        ServiceLocator.Current.Unregister<IInputService>();
        ServiceLocator.Current.Unregister<IMoveService>();
        ServiceLocator.Current.Unregister<ICameraService>();
    }
    
    void OnDestroy()
    {
        UnregisterServices();
    }
}
