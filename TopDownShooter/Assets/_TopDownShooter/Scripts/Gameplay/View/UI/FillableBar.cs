using System;
using UnityEngine;
using UnityEngine.UI;

namespace _TopDownShooter.Scripts.View
{
    [ExecuteInEditMode, RequireComponent(typeof(RectTransform))]
    public class FillableBar : MonoBehaviour
    {
        RectTransform _rectTransform;
        [SerializeField] Image _border;
        [SerializeField] Image _background;
        [SerializeField] Image _filler;
        [SerializeField, Range(0,1)] float _fillPercent;
        [SerializeField] float _borderWidth;
        [SerializeField] Color _borderColor;
        [SerializeField] Sprite _borderSprite;
        [SerializeField] Color _backgroundColor;
        [SerializeField] Sprite _backgroundSprite;
        [SerializeField] Color _fillColor;
        [SerializeField] Sprite _fillSprite;

        #region Properties
        public float BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = value;
                ChangeSize();
            }
        }
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                _border.color = value;
            }
        }
        public Sprite BorderSprite
        {
            get => _borderSprite;
            set
            {
                _borderSprite = value;
                _border.sprite = value;
            }
        }
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                _background.color = value;
            }
        }
        public Sprite BackgroundSprite
        {
            get => _backgroundSprite;
            set
            {
                _backgroundSprite = value;
                _background.sprite = value;
            }
        }
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                _filler.color = value;
            }
        }
        public Sprite FillSprite
        {
            get => _fillSprite;
            set
            {
                _fillSprite = value;
                _filler.sprite = value;
            }
        }
        public float FillPercent
        {
            get => _fillPercent;
            set
            {
                _fillPercent = Mathf.Clamp01(value);
                DrawFillPercent();
                _fillPercent = value;
            }
        }
        #endregion
        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            enabled = false;
        }
        void Update()
        {
            ChangeSize();
            _background.color = BackgroundColor;
            _filler.color = FillColor;
            _border.color = BorderColor;
            _background.sprite = BackgroundSprite;
            _filler.sprite = FillSprite;
            _border.sprite = BorderSprite;
            DrawFillPercent();
        }
        void ChangeSize()
        {
            var sizeX = _rectTransform.sizeDelta.x;
            var sizeY = _rectTransform.sizeDelta.y;
            _background.rectTransform.sizeDelta = new Vector2(sizeX - BorderWidth, sizeY - BorderWidth);
            _filler.rectTransform.sizeDelta = new Vector2(sizeX - BorderWidth, sizeY - BorderWidth);
            _border.rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
        }
        void DrawFillPercent()
        {
            var sizeX = _background.rectTransform.sizeDelta.x;
            var sizeY = _background.rectTransform.sizeDelta.y;
            _filler.rectTransform.sizeDelta = new Vector2(sizeX * FillPercent, sizeY);
            _filler.rectTransform.anchoredPosition = new Vector2(-sizeX * (1 - FillPercent) / 2, 0);
        }
    }
}