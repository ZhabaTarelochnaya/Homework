using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _MultiplayerFPS.Scripts
{
    public class AssaultRiffleView : MonoBehaviour, IWeaponView
    {
        const float TrailTravelTime = 0.05f;
        WaitForSeconds _muzzleFlashWait;
        WaitForSeconds _effectDestroyWait =  new (0.5f);
        ObjectPool<TrailRenderer> _trailRendererPool;
        ObjectPool<ParticleSystem> _particleSystemPool;
        [field: SerializeField] public Transform ShootPosition { get; private set; }
        [SerializeField] float flashDuration;
        [SerializeField] GameObject _muzzleFlash;
        [SerializeField] TrailRenderer _bulletTrailPrefab;
        [SerializeField] ParticleSystem _hitParticlesPrefab;
        [SerializeField] AudioSource _audioSource;
        void Awake()
        {
            _muzzleFlashWait = new WaitForSeconds(flashDuration);
            _trailRendererPool = new ObjectPool<TrailRenderer>(_bulletTrailPrefab);
            _particleSystemPool = new ObjectPool<ParticleSystem>(_hitParticlesPrefab);
        }
        public void SpawnMuzzleFlash()
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(_audioSource.clip, _audioSource.volume);
            StartCoroutine(Flash());
        }
        public void SpawnTrail(Vector3 endPos)
        {
            StartCoroutine(SpawnTrailRoutine(endPos));
        }
        public void SpawnHit(Vector3 hitPos, Vector3 normal)
        {
            StartCoroutine(SpawnHitRoutine(hitPos, normal));
        }
        IEnumerator Flash()
        {
            _muzzleFlash.SetActive(true);
            yield return _muzzleFlashWait;
            _muzzleFlash.SetActive(false);
        }
        // IEnumerator SpawnTrailRoutine(Vector3 endPos)
        // {
        //     var trail = _trailRendererPool.Get();
        //     trail.SetPosition(0, ShootPosition.position);
        //     trail.SetPosition(1, endPos);
        //     yield return _effectDestroyWait;
        //     _trailRendererPool.Return(trail);
        // }
        IEnumerator SpawnTrailRoutine(Vector3 endPos)
        {
            var trail = _trailRendererPool.Get();

            Vector3 startPos = ShootPosition.position;
            trail.transform.position = startPos;
            trail.Clear(); 

            float time = 0f;
            while (time < TrailTravelTime)
            {
                trail.transform.position = Vector3.Lerp(startPos, endPos, time / TrailTravelTime);
                time += Time.deltaTime;
                yield return null;
            }
            trail.transform.position = endPos;
            yield return _effectDestroyWait;
            
            _trailRendererPool.Return(trail);
        }
        IEnumerator SpawnHitRoutine(Vector3 endPos, Vector3 direction)
        {
            var hitParticles = _particleSystemPool.Get();
            hitParticles.transform.position = endPos;
            hitParticles.transform.rotation = Quaternion.LookRotation(direction);
            yield return _effectDestroyWait;
            _particleSystemPool.Return(hitParticles);
        }
    }
}