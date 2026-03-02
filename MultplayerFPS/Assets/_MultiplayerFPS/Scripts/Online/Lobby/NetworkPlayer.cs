using System;
using _MultiplayerFPS.Scripts.Online.Lobby;
using _MultiplayerFPS.Scripts.Online.Lobby.View;
using UnityEngine;
using Mirror;
using Telepathy;
using UnityEngine.SceneManagement;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-room-player
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomPlayer.html
*/

/// <summary>
/// This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
/// The RoomPrefab object of the NetworkRoomManager must have this component on it.
/// This component holds basic room player data required for the room to function.
/// Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.
/// </summary>
public class NetworkPlayer : NetworkRoomPlayer
{
    const string StartNickname = "Player";
    static readonly Color StartColor = Color.white;

    [SyncVar(hook = nameof(OnNicknameChanged)), HideInInspector] 
    public string Nickname;
    [SyncVar(hook = nameof(OnColorChanged)), HideInInspector]  
    public Color Color;
    [SyncVar] 
    public bool IsHost;
    
    LobbyPresenter _lobbyPresenter;
    [SerializeField] LobbyView _lobbyViewPrefab;
    
    public event Action<NetworkPlayer, string> ClientNicknameChanged;
    public event Action<NetworkPlayer, Color> ClientColorChanged;
    public event Action<NetworkPlayer, bool> ClientReadyChanged;

    void OnNicknameChanged(string oldNickname, string newNickname) => ClientNicknameChanged?.Invoke(this, newNickname);
    void OnColorChanged(Color oldColor, Color newColor) => ClientColorChanged?.Invoke(this, newColor);
    
    [Command]
    public void CmdSetColor(Color newColor) => Color = newColor;

    [Command]
    public void CmdSetNickname(string newNickname) => Nickname = newNickname;

    #region Start & Stop Callbacks
    public override void OnStartServer()
    {
        Nickname = StartNickname;
        Color = StartColor;
        IsHost = connectionToClient.connectionId == 0;
    }
    public override void OnStartLocalPlayer()
    {
        if (SceneManager.GetActiveScene().name != "Lobby")
            return;
        var _lobbyView = Instantiate(_lobbyViewPrefab);
        var lobbyState = FindAnyObjectByType<LobbyState>();
        _lobbyPresenter = new LobbyPresenter(lobbyState, this, _lobbyView);
    }

    #endregion
    

    #region SyncVar Hooks
    public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
    {
        ClientReadyChanged?.Invoke(this, newReadyState);
    }

    #endregion

    #region Optional UI

    public override void OnGUI() { }

    #endregion
    
}
