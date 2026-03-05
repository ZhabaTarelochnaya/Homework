using System.Collections;
using _MultiplayerFPS.Scripts.State;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Effector.Types
{
    [CreateAssetMenu(fileName = "DamageOverTimeEffect", menuName = "ScriptableObjects/Effects/DamageOverTimeEffect")]
    public class DamageOverTimeEffect : Effect
    {
        WaitForSeconds _waitForRate;
        [field: SerializeField] public int Damage { get; private set; } = 2;
        [field: SerializeField] public float Rate { get; private set; } = 4;

        void OnEnable() => _waitForRate = new WaitForSeconds(1 / Rate);

        public override IEnumerator Execute(PlayerState playerState)
        {
            while (!playerState.IsDead)
            {
                playerState.Damage(Damage);
                yield return _waitForRate;
            }
        }
    }
}