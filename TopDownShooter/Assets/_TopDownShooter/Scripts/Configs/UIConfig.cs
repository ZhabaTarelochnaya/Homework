using TMPro;
using UnityEngine;

namespace _TopDownShooter.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "ScriptableObjects/UIConfig")]
    public class UIConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject GameplayUI { get; private set; }
        [field: SerializeField] public GameObject HUD { get; private set; }
    }
}