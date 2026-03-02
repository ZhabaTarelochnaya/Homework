using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby
{
    public class PlayerCardData
    {
        public string Nickname { get; set; }
        public Color Color { get; set; }
        public bool IsReady { get; set; }

        public PlayerCardData(string nickname, Color color, bool isReady)
        {
            Nickname = nickname;
            Color = color;
            IsReady = isReady;
        }
    }
}