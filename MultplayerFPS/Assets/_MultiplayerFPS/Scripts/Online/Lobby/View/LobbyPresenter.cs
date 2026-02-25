using _MultiplayerFPS.Scripts.Online.Lobby.View;
using _MultiplayerFPS.Scripts.Utils;
using Mirror;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class LobbyPresenter : IPresenter
    {
        readonly LobbyState _lobbyState;
        readonly NetworkPlayer _player;
        readonly ILobbyView _lobbyView;
        readonly ColorPickerPresenter _colorPickerPresenter;
        readonly PlayerListPresenter _playerListPresenter;

        public LobbyPresenter(LobbyState lobbyState, NetworkPlayer player, ILobbyView lobbyView)
        {
            _lobbyState = lobbyState;
            _player = player;
            _lobbyView = lobbyView;

            _colorPickerPresenter = new ColorPickerPresenter(player, lobbyView.ColorPickerView);
            _playerListPresenter = new PlayerListPresenter(lobbyState, lobbyView.PlayerListView);
            
            _lobbyView.Enable();
            _lobbyView.ReadyPressed += LobbyViewOnReadyPressed;
            _lobbyView.NicknameEditEnded += LobbyViewOnNicknameEditEnded;
            _lobbyView.StopPressed += LobbyViewOnStopPressed;
            _lobbyView.StartGamePressed += LobbyViewOnStartGamePressed;
            _lobbyState.AllPlayersReadyChanged += OnAllPlayersReadyChanged;
        }
        public void Enable()
        {
            _lobbyView.ReadyPressed += LobbyViewOnReadyPressed;
            _lobbyView.NicknameEditEnded += LobbyViewOnNicknameEditEnded;
            _lobbyView.StopPressed += LobbyViewOnStopPressed;
            _lobbyView.StartGamePressed += LobbyViewOnStartGamePressed;
            _lobbyState.AllPlayersReadyChanged += OnAllPlayersReadyChanged;
            
            _colorPickerPresenter.Enable();
            _playerListPresenter.Enable();
            _lobbyView.Enable();
        }

        public void Disable()
        {
            _colorPickerPresenter.Disable();
            _playerListPresenter.Disable();
            _lobbyView.Disable();
            _lobbyView.ReadyPressed -= LobbyViewOnReadyPressed;
            _lobbyView.NicknameEditEnded -= LobbyViewOnNicknameEditEnded;
            _lobbyView.StopPressed -= LobbyViewOnStopPressed;
            _lobbyView.StartGamePressed -= LobbyViewOnStartGamePressed;
            _lobbyState.AllPlayersReadyChanged -= OnAllPlayersReadyChanged;
        }
        void LobbyViewOnStartGamePressed()
        {
            Disable();
            NetManager.singleton.StartGame();
        }
        void LobbyViewOnStopPressed()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetManager.singleton.StopHost(); 
            }
            else if (NetworkClient.isConnected)
            {
                NetManager.singleton.StopClient();
            }
        }
        void LobbyViewOnNicknameEditEnded(string nickname) => _player.CmdSetNickname(nickname);

        void LobbyViewOnReadyPressed() => _player.CmdChangeReadyState(!_player.readyToBegin);

        void OnAllPlayersReadyChanged(bool allPlayersReady)
        {
            if (NetworkServer.connections.Count < 2 || !NetworkServer.active) return;
            _lobbyView.SetStartGameButtonActive(allPlayersReady);
        }
    }
}