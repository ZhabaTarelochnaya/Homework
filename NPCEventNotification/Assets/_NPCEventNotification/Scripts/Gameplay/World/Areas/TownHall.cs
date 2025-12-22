using System;
using System.Collections.Generic;
using _NPCEventNotification.Scripts.Gameplay.World.NPC.Types;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class TownHall : MonoBehaviour
    {
        List<GameObject> _npcs = new ();
        [SerializeField] int _startWorkerAmount = 3;
        [SerializeField] int _startGuardAmount = 2;
        public void Bind(AllyFactory allyFactory)
        {
            for (int i = 0; i < _startWorkerAmount; i++)
            {
                allyFactory.CreateWorker(transform.position);
            }

            for (int i = 0; i < _startGuardAmount; i++)
            {
                allyFactory.CreateGuard(transform.position);
            }
        }

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