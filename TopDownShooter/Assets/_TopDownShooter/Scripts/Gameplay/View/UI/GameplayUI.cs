using System;
using _TopDownShooter.Scripts.Gameplay.Services;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class GameplayUI : MonoBehaviour
    {
        HUD _hud;
        [field: SerializeField] public RectTransform Screens { get; private set; }
        [field: SerializeField] public RectTransform Popups { get; private set; }

        public void Bind(WindowManagerService windowManagerService)
        {
            _hud = windowManagerService.OpenHUD();
        }

        public void Reset()
        {
            _hud.Reset();
        }
    }
}