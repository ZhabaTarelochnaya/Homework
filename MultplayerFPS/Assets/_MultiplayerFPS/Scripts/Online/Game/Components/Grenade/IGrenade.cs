using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public interface IGrenade
    {
        [Server]
        public void Throw(Vector3 direction);
    }
}