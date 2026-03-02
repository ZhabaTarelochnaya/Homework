namespace _MultiplayerFPS.Scripts.State
{
    public readonly struct PlayerScore
    {
        public readonly int Kills;
        public readonly int Deaths;
        public readonly int Score;

        public PlayerScore(int kills, int deaths)
        {
            Kills = kills;
            Deaths = deaths;
            Score = (int)(kills * kills / ((float)deaths + 1) * 100);
        }
    }
}