using System;
using _MultiplayerFPS.Scripts.Utils;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby.View
{
    public interface IColorPickerView : IView
    {
        public event Action<Color> ColorChanged;
    }
}