using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components.Health
{
    public class HitBox : NetworkBehaviour
    {
        [field: SerializeField] public int Damage { get; set; }
        public override void OnStartClient()
        {
            gameObject.SetActive(false);
        }
    }
}