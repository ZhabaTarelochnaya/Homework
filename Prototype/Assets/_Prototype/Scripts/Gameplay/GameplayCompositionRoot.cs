using _Prototype.Scripts.Gameplay.Controllers;
using _Prototype.Scripts.Gameplay.Controllers.CollectorController;
using _Prototype.Scripts.Gameplay.Services;
using _Prototype.Scripts.Gameplay.Services.GameDataService;
using _Prototype.Scripts.Gameplay.Services.InputService;
using _Prototype.Scripts.Gameplay.Services.MoveService;
using _Prototype.Scripts.Gameplay.Services.PickUpService;
using _Prototype.Scripts.Gameplay.View;
using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameplayCompositionRoot : MonoBehaviour
{
    ICameraService _cameraService;
    ICollectorController _collectorController;
    [SerializeField] PlayerView _playerView;
    [SerializeField] Transform _pickUps;
    [SerializeField] CollectorView _collectorView;
    public void Awake()
    {
        RegisterServices();
        
        IPlayerController playerController = new PlayerController();
        _playerView.Init(playerController);

        _collectorController = new CollectorController(_collectorView);
        
        var gameStateService = ServiceLocator.Current.Get<IGameStateService>();
        foreach (IPickUpView child in _pickUps.GetComponentsInChildren<IPickUpView>())
        {
            var pickUp = new EmptyPickUpController(child);
            gameStateService.AddPickUp(pickUp);
        }

        _cameraService.CurrentTarget = _playerView;
    }
    void RegisterServices()
    {
        var inputService = new KeyboardInputService();
        ServiceLocator.Current.Register<IInputService>(inputService);
        var moveService = new MoveService();
        ServiceLocator.Current.Register<IMoveService>(moveService);
        _cameraService = new CameraService();
        ServiceLocator.Current.Register(_cameraService);
        var collectService = new CollectService();
        ServiceLocator.Current.Register<ICollectService>(collectService);
    }
    void UnregisterServices()
    {
        ServiceLocator.Current.Unregister<IInputService>();
        ServiceLocator.Current.Unregister<IMoveService>();
        ServiceLocator.Current.Unregister<ICameraService>();
        ServiceLocator.Current.Unregister<ICollectService>();
    }
    void OnDestroy()
    {
        UnregisterServices();
    }
}
