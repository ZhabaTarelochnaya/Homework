using NPCEventNotification.Scripts.Gameplay;
using NPCEventNotification.Scripts.Gameplay.NPC;
using NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection;
using NPCEventNotification.Scripts.Gameplay.World.NPC;
using NPCEventNotification.Scripts.Gameplay.World.NPC.Types.Warden;
using UnityEngine;

namespace _NPCEventNotification.Scripts.Gameplay.World.NPC.Types
{
    public class AllyFactory
    {
        readonly AllyFactorySO _allyFactory;
        readonly EventManager _eventManager;
        readonly ResourceZone _resourceZone;
        readonly TownHall _townHall;
        readonly Transform _storage;

        public AllyFactory(AllyFactorySO allyFactory, EventManager eventManager, 
            ResourceZone resourceZone, TownHall townHall, Transform storage)
        {
            _allyFactory = allyFactory;
            _eventManager = eventManager;
            _resourceZone = resourceZone;
            _townHall = townHall;
            _storage = storage;
        }

        public WorkerController CreateWorker(Vector2 position)
        {
            var worker = _allyFactory.CreateWorker(position);
            worker.Bind(_eventManager, _resourceZone, _townHall, _storage);
            return worker;
        }

        public GuardController CreateGuard(Vector2 position)
        {
            var guard = _allyFactory.CreateGuard(position);
            return guard;
        }
    }
}