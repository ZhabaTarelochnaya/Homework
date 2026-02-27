using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public interface IWeaponView
    {
        public Transform ShootPosition { get; }
        public void SpawnMuzzleFlash();
        public void SpawnTrail(Vector3 endPos);
        void SpawnHit(Vector3 hitPos, Vector3 normal);
    }
}