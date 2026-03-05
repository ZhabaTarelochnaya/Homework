using System.Collections;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Effector.Types
{
    [CreateAssetMenu(fileName = "JumpHeightAssignEffect", menuName = "ScriptableObjects/Effects/JumpHeightAssignEffect")]
    public class JumpHeightAssignEffect : Effect
    {
        [field: SerializeField] public float HeightMultiplier { get; private set; } = 0.3f;
        public override IEnumerator Execute(PlayerState playerState)
        {
            var baseHeight = ServiceLocator.Current.Get<IConfigService>()
                .Get<PlayerConfig>()
                .JumpHeight;
            playerState.JumpHeight = baseHeight * HeightMultiplier;
            yield return null;
        }

        public override void OnRemove(PlayerState playerState)
        {
            var baseHeight = ServiceLocator.Current.Get<IConfigService>()
                .Get<PlayerConfig>()
                .JumpHeight;
            playerState.JumpHeight = baseHeight;
        }
    }
}