using System;
using _Prototype.Scripts.Gameplay.Services;
using _Prototype.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View
{
    public class CameraView : MonoBehaviour
    {
        ICameraService _cameraService;

        void Awake()
        {
            _cameraService = ServiceLocator.Current.Get<ICameraService>();
        }

        void LateUpdate()
        {
            _cameraService.LateUpdate();
        }
    }
}