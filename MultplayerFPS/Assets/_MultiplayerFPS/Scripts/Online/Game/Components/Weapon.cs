using System;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Config.Weapon;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public class Weapon : NetworkBehaviour
    {
        float _shootTimer;
        float _effectsTimer;
        IInputService _inputService;
        [SerializeField] Transform _hand;
        [SerializeField] LayerMask _hitLayerMask;
        public IWeaponConfig Config { get; set; }
        public IWeaponView View { get; set; }
        public Transform ShootOrigin => View.ShootPosition;

        void Start()
        {
            var playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
            Config = (IWeaponConfig)playerConfig.StartingWeapon;
            if (isClient)
            {
                var instance = Instantiate(Config.Prefab, _hand);
                View = instance.GetComponent<IWeaponView>();
            }
        }
        public void Init()
        {
            _inputService = ServiceLocator.Current.Get<IInputService>();
        }
        void Update()
        {
            if (isServer && _shootTimer > 0)
            {
                _shootTimer -= Time.deltaTime;
            }
            
            if (!isClient || _inputService == null) return;
            
            if (_effectsTimer > 0)
            {
                _effectsTimer -= Time.deltaTime;
            }
            else if (_inputService.GetShootButton())
            {
                _effectsTimer = 1 / Config.FireRate;
                View.PlayShootSound();
                View.ShowMuzzleFlash();
            }
        }
        [Command]
        public void CmdShoot(Vector3 origin, Vector3 direction)
        {
            if (_shootTimer > 0) return;
            _shootTimer = 1 / Config.FireRate;
            if (Physics.Raycast(origin, direction, out RaycastHit hit, Config.Range, _hitLayerMask))
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
        [ClientRpc]
        void RpcSpawnTrail(Vector3 hitPos)
        {
            View.SpawnTrail(hitPos);
        }
        [ClientRpc]
        void RpcSpawnHit(Vector3 hitPos, Vector3 normal)
        {
            View.SpawnHit(hitPos, normal);
        }
    }
}