using System.Collections;
using _MultiplayerFPS.Scripts.Components.Health;
using _MultiplayerFPS.Scripts.Config.Pickup;
using _MultiplayerFPS.Scripts.Services;
using _MultiplayerFPS.Scripts.Services.Config;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public class Grenade : NetworkBehaviour
    {
        GrenadeConfig _config;
        Rigidbody _rigidBody;
        Vector3 _direction;
        IPlayerScoreService _playerScoreService;
        [SerializeField] ParticleSystem[] _particles;
        [SerializeField] GameObject _model;
        [SerializeField] AudioSource _audioSource;
        public override void OnStartServer()
        {
            _playerScoreService = ServiceLocator.Current.Get<IPlayerScoreService>();
            _config = ServiceLocator.Current.Get<IConfigService>().Get<GrenadeConfig>();
            _rigidBody = GetComponent<Rigidbody>();
        }
        [Server]
        public void Throw(Vector3 direction, uint playerNetId)
        {
            _rigidBody.isKinematic = false;
            var velocity = direction * _config.Speed;
            _rigidBody.AddForce(velocity, ForceMode.VelocityChange);
            StartCoroutine(Explode(playerNetId));
        }
        [ClientRpc]
        public void RpcSetActive(bool active)
        {
            gameObject.SetActive(active);
            _model.SetActive(active);
        }
        IEnumerator Explode(uint playerNetId)
        {
            yield return new WaitForSeconds(_config.ExplosionDelay);
            _rigidBody.isKinematic = true;
            _rigidBody.rotation = Quaternion.identity;
            _model.SetActive(false);
            RpcExplode();
            Collider[] hits = Physics.OverlapSphere(
                transform.position,
                _config.Radius,
                _config.HarmedLayers
            );
            foreach (var hit in hits)
            {
                if (IsBlockedByWall(hit)) continue;
                
                var health = hit.GetComponentInParent<Health.Health>();
                if (!health) continue;
                
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                float normalized = distance / _config.Radius;

                float damage = Mathf.Lerp(
                    _config.MaxDamage,
                    _config.MinDamage,
                    normalized
                );
                health.Damage((int)damage);
                if (!health.IsDead) continue;
                var hitPlayerId = health.netId;
                if (hitPlayerId == playerNetId) continue;
                _playerScoreService.AddKill(playerNetId);
            }

            yield return new WaitForSeconds(_config.ExplosionDuration);
            _rigidBody.isKinematic = false;
            _model.SetActive(true);
        }
        [ClientRpc]
        void RpcExplode()
        {
            _audioSource.PlayOneShot(_audioSource.clip, _audioSource.volume);
            foreach (var particle in _particles)
            {
                particle.Play();
            }
        }
        bool IsBlockedByWall(Collider hit)
        {
            if (Physics.Linecast(
                    transform.position,
                    hit.transform.position,
                    out RaycastHit block,
                    _config.DamageBlockingLayers))
            {
                if (block.collider.transform != hit.transform) return true;
            }
            return false;
        }
    }
}