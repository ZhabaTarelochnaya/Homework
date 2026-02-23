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
        _netManager.LocalClientConnected += NetManagerOnLocalClientConnected;
        _netManager.LocalClientConnecting += NetManagerOnLocalClientConnecting;
        _netManager.LocalClientDisconnected += NetManagerOnLocalClientDisconnected;
    }
    void NetManagerOnLocalClientConnecting() => _statusText.text = "Status: Connecting...";
    void NetManagerOnLocalClientConnected() => _statusText.text = "Status: Connected";
    void NetManagerOnLocalClientDisconnected() => _statusText.text = "Status: Disconnected";
}
