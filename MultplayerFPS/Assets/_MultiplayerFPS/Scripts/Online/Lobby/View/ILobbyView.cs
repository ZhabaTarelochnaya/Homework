using System;
using _MultiplayerFPS.Scripts.Online.Lobby.View;
using _MultiplayerFPS.Scripts.Utils;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public interface ILobbyView : IView
    {
        public IPlayerListView PlayerListView { get; }
        public IColorPickerView ColorPickerView { get; }
        
        public event Action ReadyPressed;
        public event Action<string> NicknameEditEnded;
        public event Action StopPressed;
        public event Action StartGamePressed;
        
        public void SetStartGameButtonActive(bool isActive);
    }
}