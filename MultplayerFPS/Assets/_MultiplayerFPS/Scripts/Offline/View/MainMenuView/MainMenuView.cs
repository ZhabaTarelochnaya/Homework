using System;
using _MultiplayerFPS.Scripts.Offline.View.MainMenuView;
using UnityEngine;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    [SerializeField] GameObject _selectLobbyPanel;
    public event Action ExitButtonClicked;
    public event Action HostButtonClicked;
    public event Action DiscoverButtonClicked;
    public event Action ClientButtonClicked;
    public event Action<string> IpInputFieldEndEdit;
    
    public void OnExitButtonClicked() => ExitButtonClicked?.Invoke();
    public void OnDiscoverButtonClicked() => DiscoverButtonClicked?.Invoke();
    public void OnHostButtonClicked() => HostButtonClicked?.Invoke();
    public void OnClientButtonClicked() => ClientButtonClicked?.Invoke();
    public void OnIPInputFieldEndEdit(string newIP) => IpInputFieldEndEdit?.Invoke(newIP);
    public void Enable() => gameObject.SetActive(true);
    public void Disable() => gameObject.SetActive(false);
}
