using System;
using _MultiplayerFPS.Scripts.Utils;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Leaderboard
{
    public interface ILeaderboardView : IView
    {
        public event Action ReturnToLobbyButtonPressed;
        public void UpdateData(LeaderboardData[] leaderboardData);
        public void SetReturnToLobbyButtonActive(bool active);
    }
}