using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _TopDownShooter.Scripts.View
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Renderer))]
    public class EnemyView : MonoBehaviour
    {
        EnemyController _enemyController;
        Renderer _renderer;
        Color _originalColor;
        Coroutine _hurtCoroutine;
        [SerializeField] float _flashDuration = 0.5f;
        [field: SerializeField] public HurtBox HurtBox { get; private set; }
        [field: SerializeField] public HitBox HitBox { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        public void Bind(EnemyController enemyController)
        {
            _enemyController = enemyController;
            StartCoroutine(UpdatePath());
        }

        void HurtBoxOnHit(int damage, int currentHealth)
        {
            if (_hurtCoroutine != null)
            {
                StopCoroutine(_hurtCoroutine);
            }
            _hurtCoroutine = StartCoroutine(HurtColorChange());
        }

        void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            _renderer = GetComponent<Renderer>();
            _originalColor = _renderer.material.color;
            HurtBox.Hit += HurtBoxOnHit;
        }

        IEnumerator UpdatePath()
        {
            while (true)
            {
                _enemyController.UpdatePath();
                yield return new WaitForSeconds(1 / _enemyController.AIPathfindingFrequency);
            }
        }

        void OnDestroy()
        {
            HurtBox.Hit -= HurtBoxOnHit;
        }

        IEnumerator HurtColorChange()
        {
            float elapsedTime = 0f;
            
            while (elapsedTime < _flashDuration)
            {
                float t = elapsedTime / _flashDuration;
                Color currentColor = Color.Lerp(Color.white, _originalColor, t);
            
                _renderer.material.color = currentColor;
            
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        
            _renderer.material.color = _originalColor;
        
            _hurtCoroutine = null;
        }
    }
}