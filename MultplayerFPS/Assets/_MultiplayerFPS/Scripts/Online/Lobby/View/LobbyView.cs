using System;
using _MultiplayerFPS.Scripts.Online.Lobby.View;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class LobbyView : MonoBehaviour, ILobbyView
    {
        LobbyState _lobbyState;
        [SerializeField] PlayerListView _playerListView;
        [SerializeField] ColorPickerView _colorPanelView;
        [SerializeField] Button _startGameButton;
        [SerializeField] TMP_Text _readyButtonText;
        
        public IPlayerListView PlayerListView => _playerListView;
        public IColorPickerView ColorPickerView => _colorPanelView;

        public event Action ReadyPressed;
        public event Action<string> NicknameEditEnded;
        public event Action StopPressed;
        public event Action StartGamePressed;
        

        public void Enable()
        {
            gameObject.SetActive(true);
            _startGameButton.gameObject.SetActive(false);
        }
        public void Disable()
        {
            gameObject.SetActive(false);
        }
        public void SetStartGameButtonActive(bool isActive) => _startGameButton.gameObject.SetActive(isActive);
        public void OnReadyButtonPressed()
        {
            _readyButtonText.text = _readyButtonText.text == "Unready" ? "Ready" : "Unready";
            ReadyPressed?.Invoke();
        }
        public void OnNicknameInputFieldEndEdit(string newNickname) => NicknameEditEnded?.Invoke(newNickname);
        public void OnStartGameButtonPressed() => StartGamePressed?.Invoke();
        public void OnStopButtonPressed() => StopPressed?.Invoke();
    }
}