using _MultiplayerFPS.Scripts.Utils;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public interface IPlayerListView : IView
    {
        public void UpdateData(PlayerCardData?[] playersData);
    }
}