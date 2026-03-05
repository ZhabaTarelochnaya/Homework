using _MultiplayerFPS.Scripts.State;

namespace _MultiplayerFPS.Scripts.Leaderboard
{
    public readonly struct LeaderboardData
    {
        public readonly string Nickname;
        public readonly int Kills;
        public readonly int Deaths;
        public readonly int Score;

        public LeaderboardData(string nickname, PlayerScore score)
        {
            Nickname = nickname;
            Kills = score.Kills;
            Deaths = score.Deaths;
            Score = score.Score;
        }
    }
}