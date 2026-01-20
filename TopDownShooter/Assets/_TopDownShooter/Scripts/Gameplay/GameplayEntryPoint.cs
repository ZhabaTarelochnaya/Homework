using _TopDownShooter.Scripts;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.View;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] PlayerView _playerView;
    public void Bind(UIRoot uiRoot)
    {
        GameplayServiceRegistrations.Register();
        BindPlayer();
    }

    void BindPlayer()
    {
        var playerController = new PlayerController(_playerView.Rigidbody);
        _playerView.Bind(playerController);
    }
    void OnDestroy()
    {
        GameplayServiceRegistrations.Unregister();
    }
}
