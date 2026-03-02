using System;
using System.Collections;
using System.Linq;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config;
using _MultiplayerFPS.Scripts.Config.Weapon;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public class Weapon : NetworkBehaviour
    {
        float _shootTimer;
        float _reloadTimer;
        IStateService _stateService;
        IPlayerScoreService _playerScoreService;
        PlayerState _playerState;
        Coroutine _reloadCoroutine;
        [SerializeField] Transform _hand;
        [SerializeField] LayerMask _hitLayerMask;
        [SerializeField] AudioSource _hitSoundSource;
        public IWeaponConfig Config { get; set; }
        public IWeaponView View { get; set; }
        public Transform ShootOrigin => View.ShootPosition;
        public int CurrentAmmo => _stateService.GetPlayerState(netId).CurrentAmmo;
       

        public override void OnStartServer()
        {
            var playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
            Config = (IWeaponConfig)playerConfig.StartingWeapon;
            _stateService = ServiceLocator.Current.Get<IStateService>();
            _playerScoreService = ServiceLocator.Current.Get<IPlayerScoreService>();
            _playerState = _stateService.GetPlayerState(netId);
            _playerState.CurrentAmmo = Config.MaxAmmo;
        }

        public override void OnStartClient()
        {
            if (isLocalPlayer)
            {
                _stateService = ServiceLocator.Current.Get<IStateService>();
            }
            var playerConfig = ServiceLocator.Current.Get<IConfigService>().Get<PlayerConfig>();
            Config = (IWeaponConfig)playerConfig.StartingWeapon;
            var instance = Instantiate(Config.Prefab, _hand);
            View = instance.GetComponent<IWeaponView>();
        }
        void Update()
        {
            if (isServer && _shootTimer > 0)
            {
                _shootTimer -= Time.deltaTime;
            }
        }

        [Command]
        public void CmdStopShoot() => _playerState.IsShooting = false;

        [Command]
        public void CmdStopReload()
        {
            _playerState.IsReloading = false;
            StopCoroutine(_reloadCoroutine);
        }
        [Command]
        public void CmdStartReload()
        {
            if (_reloadCoroutine != null)
            {
                StopCoroutine(_reloadCoroutine);
            }
            _reloadCoroutine = StartCoroutine(Reload());
        }
        [Command]
        public void CmdShoot(Vector3 origin, Vector3 direction)
        {
            if (_shootTimer > 0) return;
            _shootTimer = 1 / Config.FireRate;
            _playerState.CurrentAmmo--;
            _playerState.IsShooting = true;
            PlayShootEffects(connectionToClient);

            var hit = Raycast(origin, direction);
            
            if (hit.HasValue)
            {
                var value = hit.Value;
                RpcSpawnTrail(value.point);
                if (value.collider)
                {
                    RpcSpawnHit(value.point, value.normal);
                }
                if (value.collider.TryGetComponent(out HurtBox hurtBox))
                {
                    hurtBox.Damage(Config.Damage);
                    PlayTargetHitSound(connectionToClient);
                    if (!hurtBox.Health.IsDead) return;
                    
                    _playerScoreService.AddKill(netId);
                }
            }
            else
            {
                var endPos = origin + direction * Config.Range;
                RpcSpawnTrail(endPos);
            }
        }

        RaycastHit? Raycast(Vector3 origin, Vector3 direction)
        {
            Ray ray = new Ray(origin, direction);
            var hits = Physics.RaycastAll(ray, Config.Range, _hitLayerMask);
            RaycastHit? closestHit = null;
            float closestDistance = float.MaxValue;
            for (int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                if (hit.collider.transform.root == transform)
                    continue;
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    closestHit = hit;
                }
            }
            return closestHit;
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
        [TargetRpc]
        void PlayTargetHitSound(NetworkConnectionToClient conn)
        {
            _hitSoundSource.PlayOneShot(_hitSoundSource.clip, _hitSoundSource.volume);
        }
        [TargetRpc]
        void PlayShootEffects(NetworkConnectionToClient conn)
        {
            View.ShowMuzzleFlash();
            View.PlayShootSound();
        }
        IEnumerator Reload()
        {
            _playerState.IsReloading = true;
            yield return new WaitForSeconds(Config.ReloadTime);
            _playerState.CurrentAmmo = Config.MaxAmmo;
            _playerState.IsReloading = false;
        }
    }
}