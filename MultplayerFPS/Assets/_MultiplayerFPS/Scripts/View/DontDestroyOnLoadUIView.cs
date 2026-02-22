using System;
using _MultiplayerFPS.Scripts.Utils;
using TMPro;
using UnityEngine;

public class DontDestroyOnLoadUIView : MonoBehaviour
{
    NetManager _netManager;
    [SerializeField] TMP_Text _statusText;
    
    [field: SerializeField] public LoadingScreen LoadingScreen { get; private set; }
    [field: SerializeField] public ExceptionPopupView ExceptionPopupView { get; private set; }
    public void Init(NetManager netManager)
    {
        _netManager = netManager;
        netManager.LocalClientConnected += NetManagerOnLocalClientConnected;
        netManager.LocalClientConnecting += NetManagerOnLocalClientConnecting;
        netManager.LocalClientDisconnected += NetManagerOnLocalClientDisconnected;
    }
    void NetManagerOnLocalClientConnecting() => _statusText.text = "Connecting...";
    void NetManagerOnLocalClientConnected() => _statusText.text = "Connected";
    void NetManagerOnLocalClientDisconnected() => _statusText.text = "Disconnected";
}
