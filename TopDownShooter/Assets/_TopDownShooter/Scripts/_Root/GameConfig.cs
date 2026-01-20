using _TopDownShooter.Scripts.Gameplay.Configs;
using UnityEngine;

namespace _TopDownShooter.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public GameplayConfig GameplayConfig { get; private set; }
    }
}