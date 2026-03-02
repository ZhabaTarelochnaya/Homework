using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace _MultiplayerFPS.Scripts.Leaderboard
{
    public class LeaderboardView : MonoBehaviour, ILeaderboardView
    {
        [SerializeField] PlayerRowView[] _rowViews;
        public void UpdateData(LeaderboardData[] leaderboardData)
        {
            for (int i = 0; i < _rowViews.Length; i++)
            {
                if (leaderboardData[i] == null)
                {
                    _rowViews[i].Disable();
                    continue;
                }
                _rowViews[i].Enable();
                _rowViews[i].UpdateData(i + 1, leaderboardData[i]);
            }
        }
        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);
    }
}