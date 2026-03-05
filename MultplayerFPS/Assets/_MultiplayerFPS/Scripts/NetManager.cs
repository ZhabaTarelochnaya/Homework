using System;
using _MultiplayerFPS.Scripts;
using _MultiplayerFPS.Scripts.Online.Lobby;
using _MultiplayerFPS.Scripts.Utils.ExceptionPopUp;
using _MultiplayerFPS.Scripts.Utils.LoadingScreen;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;
using Mirror;
using Mirror.Examples.Common;


[DefaultExecutionOrder(-500)]
public class NetManager : NetworkRoomManager
{
    public static new NetManager singleton => (NetManager)NetworkRoomManager.singleton;
    
    ILoadingScreenService _loadingScreen;
    LobbyState _lobbyState;
    [SerializeField] LobbyState _lobbyStatePrefab;
    public event Action LocalClientConnected;
    public event Action LocalClientConnecting;
    public event Action LocalClientDisconnected;
    
    public override void Awake()
    {
        base.Awake();
        _loadingScreen = ServiceLocator.Current.Get<ILoadingScreenService>();
    }
    [Server]
    public void StartGame() => ServerChangeScene(GameplayScene);
    public string GetHostName() => _lobbyState.Players[0].Nickname; 

    #region Server Callbacks
    public override void OnRoomServerDisconnect(NetworkConnectionToClient conn)
    {
        _lobbyState.Players.Remove(conn.identity.GetComponent<NetworkPlayer>());
    }
    public override GameObject OnRoomServerCreateRoomPlayer(NetworkConnectionToClient conn)
    {
        GameObject playerObject = Instantiate(roomPlayerPrefab.gameObject);
        var player = playerObject.GetComponent<NetworkPlayer>();
        NetworkServer.Spawn(playerObject, conn);
        _lobbyState = FindObjectOfType<LobbyState>();
        _lobbyState.Players.Add(player);
        return playerObject;
    }
    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, 
        GameObject roomPlayer, GameObject gamePlayer)
    {
        var networkPlayer =  roomPlayer.GetComponent<NetworkPlayer>();
        var gameNetworkPlayer = gamePlayer.GetComponent<GameNetworkPlayer>();
        gameNetworkPlayer.Init(networkPlayer.Nickname, networkPlayer.Color);
        Debug.Log(FindFirstObjectByType<GameRoot>());
        NetworkServer.Destroy(roomPlayer);
        return true;
    }
    public override void OnRoomServerPlayersReady() => _lobbyState.AllPlayersReady = allPlayersReady;
    public override void OnRoomServerPlayersNotReady() => _lobbyState.AllPlayersReady = allPlayersReady;

    #endregion

    #region Client Callbacks
    
    public override void OnRoomClientSceneChanged()
    {
        _loadingScreen.Hide();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        LocalClientConnecting?.Invoke();
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        LocalClientConnected?.Invoke();
    }

    public override void OnClientTransportException(Exception exception)
    {
        ServiceLocator.Current.Get<IExceptionUIService>().ShowError(exception.GetType().Name, exception.Message);
        base.OnClientTransportException(exception);
    }
    public override void OnClientError(TransportError error, string reason)
    {
        ServiceLocator.Current.Get<IExceptionUIService>().ShowError(error.ToString(), reason);
        base.OnClientError(error, reason);
    }
    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        LocalClientDisconnected?.Invoke();
    }

    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        base.OnClientChangeScene(newSceneName, sceneOperation, customHandling);
        if (NetworkClient.activeHost) return;
        _loadingScreen.Show();
    }
    public override void OnClientSceneChanged()
    {
        base.OnClientSceneChanged();
        _loadingScreen.Hide();
    }
    
    #endregion
    #region Optional UI

    public override void OnGUI() { }

    #endregion
}
