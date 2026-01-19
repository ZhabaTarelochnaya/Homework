using UnityEngine;

namespace DefaultNamespace.Gameplay.Data
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "ScriptableObjects/UIConfig")]
    public class UIConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject GameplayUI { get; private set; }
        [field: SerializeField] public GameObject HUDPrefab { get; private set; }
        [field: SerializeField] public GameObject WinPopUpPrefab { get; private set; }
        [field: SerializeField] public GameObject LosePopUpPrefab { get; private set; }
    }
}