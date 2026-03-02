using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public readonly struct PlayerCardData
    {
        public string Nickname { get; }
        public Color Color { get; }
        public bool IsReady { get; }

        public PlayerCardData(string nickname, Color color, bool isReady)
        {
            Nickname = nickname;
            Color = color;
            IsReady = isReady;
        }
    }
}