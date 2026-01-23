using System;
using UnityEngine;

namespace _TopDownShooter.Scripts.View
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] ParticleSystem[] _particleSystems;

        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Explode();
            }
        }

        public void Explode()
        {
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
        }
    }
}