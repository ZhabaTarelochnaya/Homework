using System;
using _TopDownShooter.Scripts.Gameplay.Configs;
using _TopDownShooter.Scripts.Gameplay.Controllers;
using _TopDownShooter.Scripts.Gameplay.Services;
using _TopDownShooter.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class WeaponView : MonoBehaviour
    {
        WeaponManager _weaponManager;
        [field: SerializeField] public Transform ShootPosition { get; private set; }
        public void Bind(WeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
        }
        void Update()
        {
            _weaponManager.Update();
        }
    }
}