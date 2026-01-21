using System;
using System.Collections;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class EnemySpawnerView : MonoBehaviour
    {
        EnemySpawnerController _controller;
        [field: SerializeField] public Transform SpawnPoints { get; private set; }
        [field: SerializeField] public Transform EnemiesParent {get; private set;}

        public void Bind(EnemySpawnerController controller)
        {
            _controller = controller;
            StartCoroutine(_controller.Spawn());
        }
    }
}