using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 Offset { get; private set; }
        [field: SerializeField] public Vector3 Rotation { get; private set; }
    }
}