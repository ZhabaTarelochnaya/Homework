using System;
using UnityEngine;
using UnityEngine.UI;

namespace _MultiplayerFPS.Scripts.Online.Lobby.View
{
    public class ColorPickerView : MonoBehaviour, IColorPickerView
    {
        [SerializeField] Slider _redSlider;
        [SerializeField] Slider _greenSlider;
        [SerializeField] Slider _blueSlider;
        [SerializeField] Image _colorSample;
        
        public event Action<Color> ColorChanged;
        public void Enable()
        {
            _redSlider.onValueChanged.AddListener(OnColorChanged);
            _greenSlider.onValueChanged.AddListener(OnColorChanged);
            _blueSlider.onValueChanged.AddListener(OnColorChanged);
        }
        public void Disable()
        {
            _redSlider.onValueChanged.RemoveListener(OnColorChanged);
            _greenSlider.onValueChanged.RemoveListener(OnColorChanged);
            _blueSlider.onValueChanged.RemoveListener(OnColorChanged);
        }
        public void OnOkButtonPressed()
        {
            ColorChanged?.Invoke(_colorSample.color);
        }
        void OnColorChanged(float value)
        {
            _colorSample.color = new Color(_redSlider.value, _greenSlider.value, _blueSlider.value);
        }
    }
}