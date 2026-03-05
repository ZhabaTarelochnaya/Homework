using System.Collections;
using _MultiplayerFPS.Scripts.State;
using Unity.VisualScripting;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Effector.Types
{
    [CreateAssetMenu(fileName = "SpeedChangeEffect", menuName = "ScriptableObjects/Effects/SpeedChangeEffect")]
    public class SpeedChangeEffect : Effect
    {
        [field: SerializeField] public float AdditionalSpeed { get; private set; } = -5f;
        public override IEnumerator Execute(PlayerState playerState)
        {
            playerState.Speed += AdditionalSpeed;
            yield return null;
        }

        public override void OnRemove(PlayerState playerState)
        {
            playerState.Speed -= AdditionalSpeed;
        }
    }
}