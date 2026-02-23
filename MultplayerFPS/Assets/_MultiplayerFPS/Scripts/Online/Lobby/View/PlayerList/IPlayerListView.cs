using _MultiplayerFPS.Scripts.Utils;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public interface IPlayerListView : IView
    {
        public void CreatePlayerCard(uint netId, string nickName, Color color, bool ready);
        public void RemovePlayerCard(uint netId);
        public void SetPlayerNickname(uint netId, string nickName);
        public void SetPlayerColor(uint netId, Color color);
        public void SetPlayerReady(uint netId, bool ready);
    }
}