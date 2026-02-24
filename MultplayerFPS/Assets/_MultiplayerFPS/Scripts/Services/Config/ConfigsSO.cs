using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.Config
{
    [CreateAssetMenu(fileName = "ConfigsSO", menuName = "ConfigsSO")]
    public class ConfigsSO : ScriptableObject
    {
        [field: SerializeField] public ScriptableObject[] Configs { get; private set; }
    }
}