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
        [SerializeField, Range(0,1)] float fillPercent;
        [field: SerializeField] public float BorderWidth { get; set; }

        [field: SerializeField] public Color BorderColor { get; set; }
        [field: SerializeField] public Color BackGroundColor { get; set; }
        [field: SerializeField] public Color FillColor { get; set; }
        public float FillPercent
        {
            get => fillPercent;
            set
            {
                fillPercent = Mathf.Clamp01(value);
                DrawFillPercent();
                fillPercent = value;
            }
        }
        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        void Update()
        {
            var sizeX = _rectTransform.sizeDelta.x;
            var sizeY = _rectTransform.sizeDelta.y;
            _background.rectTransform.sizeDelta = new Vector2(sizeX - BorderWidth, sizeY - BorderWidth);
            _filler.rectTransform.sizeDelta = new Vector2(sizeX - BorderWidth, sizeY - BorderWidth);
            _border.rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            _background.color = BackGroundColor;
            _filler.color = FillColor;
            _border.color = BorderColor;
            DrawFillPercent();

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