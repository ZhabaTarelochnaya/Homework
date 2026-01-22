using UnityEngine;
using UnityEngine.AI;

namespace _TopDownShooter.Scripts.View
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyView : MonoBehaviour
    {
        EnemyController _enemyController;
        [field: SerializeField] public HurtBox HurtBox { get; private set; }
        [field: SerializeField] public HitBox HitBox { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        public void Bind(EnemyController enemyController)
        {
            _enemyController = enemyController;
        }
        void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }

        void FixedUpdate()
        {
            _enemyController.FixedUpdate();
        }
        
    }
}