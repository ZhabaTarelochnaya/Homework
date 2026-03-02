using _MultiplayerFPS.Scripts.Utils;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Leaderboard
{
    public interface ILeaderboardView : IView
    {
        public void UpdateData(LeaderboardData[] leaderboardData);
    }
}