using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class TownHall : MonoBehaviour
    {
        List<GameObject> _npcs = new ();

        public void ReleaseAll()
        {
            while (_npcs.Count > 0)
            {
                _npcs[0].SetActive(true);
                _npcs.RemoveAt(0);
            }
        }

        public void Release(GameObject npc)
        {
            if (_npcs.Remove(npc))
            {
                npc.SetActive(true);
            }
            else
            {
                Debug.LogError($"TownHall {gameObject.name} does not contain {npc.name}");
            }
        }

        public void Hide(GameObject npc)
        {
            _npcs.Add(npc);
            npc.SetActive(false);
        }
    }
}