using System.Collections;
using _MultiplayerFPS.Scripts.State;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Config.Effector
{
    public abstract class Effect : ScriptableObject
    {
        public abstract IEnumerator Execute(PlayerState playerState);
    }
}