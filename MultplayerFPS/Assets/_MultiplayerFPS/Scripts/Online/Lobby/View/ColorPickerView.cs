using UnityEngine;
using UnityEngine.UI;

namespace _MultiplayerFPS.Scripts.Online.Lobby.View
{
    public class ColorPickerView : MonoBehaviour
    {
        NetworkPlayer _owner;
        [SerializeField] Slider _redSlider;
        [SerializeField] Slider _greenSlider;
        [SerializeField] Slider _blueSlider;
        [SerializeField] Image _colorSample;
        public void Init(NetworkPlayer owner)
        {
            _owner = owner;
            _redSlider.onValueChanged.AddListener(OnColorChanged);
            _greenSlider.onValueChanged.AddListener(OnColorChanged);
            _blueSlider.onValueChanged.AddListener(OnColorChanged);
        }
        public void OnOkButtonPressed()
        {
            _owner.CmdSetColor(_colorSample.color);
        }
        void OnColorChanged(float value)
        {
            _colorSample.color = new Color(_redSlider.value, _greenSlider.value, _blueSlider.value);
        }
    }
}