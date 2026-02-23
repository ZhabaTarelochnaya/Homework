using _MultiplayerFPS.Scripts.Utils;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Online.Lobby.View
{
    public class ColorPickerPresenter : IPresenter
    {
        readonly NetworkPlayer _player;
        readonly IColorPickerView _view;

        public ColorPickerPresenter(NetworkPlayer player, IColorPickerView view)
        {
            _player = player;
            _view = view;
            Enable();
        }
        public void Enable()
        {
            _view.ColorChanged += ViewOnColorChanged;
            _view.Enable();
        }
        public void Disable()
        {
            _view.Disable();
            _view.ColorChanged -= ViewOnColorChanged;
        }
        void ViewOnColorChanged(Color color) => _player.CmdSetColor(color);
    }
}