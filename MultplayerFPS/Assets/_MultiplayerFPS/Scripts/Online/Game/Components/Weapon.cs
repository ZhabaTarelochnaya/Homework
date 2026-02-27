using System;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config.Weapon;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public class Weapon : NetworkBehaviour
    {
        IWeaponView _view;
        float _timer;
        [SerializeField] GameObject _weaponView;
        [SerializeField] ScriptableObject _weaponConfig;
        [SerializeField] LayerMask _layerMask;
        public IWeaponConfig Config { get; set; }
        public Transform ShootSource => _view.ShootPosition;
        void Awake()
        {
            _view = _weaponView.GetComponent<IWeaponView>();
            Config = (IWeaponConfig)_weaponConfig;
        }

        void Update()
        {
            if (isServer && _timer > 0)
            {
                _timer -= Time.deltaTime;
            }
        }
        [Command]
        public void CmdShoot(Vector3 origin, Vector3 direction)
        {
            if (_timer > 0) return;
            _timer = 1 / Config.FireRate;
            SpawnMuzzleFlash(connectionToClient);
            if (Physics.Raycast(origin, direction, out RaycastHit hit, Config.Range, _layerMask))
            {
                if (hit.collider.TryGetComponent(out HurtBox hurtBox))
                {
                    hurtBox.Damage(Config.Damage);
                }
                RpcSpawnTrail(hit.point);
                RpcSpawnHit(hit.point, hit.normal);
            }
            else
            {
                var endPos = origin + direction * Config.Range;
                RpcSpawnTrail(endPos);
            }
        }

        [TargetRpc]
        void SpawnMuzzleFlash(NetworkConnectionToClient target)
        {
            _view.SpawnMuzzleFlash();
        }
        [ClientRpc]
        void RpcSpawnTrail(Vector3 hitPos)
        {
            _view.SpawnTrail(hitPos);
        }
        [ClientRpc]
        void RpcSpawnHit(Vector3 hitPos, Vector3 normal)
        {
            _view.SpawnHit(hitPos, normal);
        }
    }
}