using System.Collections.Generic;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "ScriptableObjects/GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public CameraConfig CameraConfig { get; private set; }
        [field: SerializeField] public List<WeaponConfig> WeaponConfigs { get; private set; }
        [field: SerializeField] public LayerMask AimGroundMask { get; private set; }
        [field: SerializeField] public LayerMask BulletMask { get; private set; }
    }
}