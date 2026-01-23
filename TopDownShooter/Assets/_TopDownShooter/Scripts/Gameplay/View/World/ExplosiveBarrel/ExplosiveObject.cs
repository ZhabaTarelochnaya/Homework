using System.Collections;
using _TopDownShooter.Scripts.View;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    [SerializeField] float _explosionRadius; 
    [SerializeField] Renderer _renderer;
    [SerializeField] Explosion _explosion;
    [SerializeField] HurtBox _hurtBox;
    [SerializeField] HitBox _hitBox;
    void Start()
    {
        _hurtBox.Died += HurtBoxOnDied;
    }
    void HurtBoxOnDied()
    {
        _hurtBox.Died -= HurtBoxOnDied;
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        _renderer.enabled = false;
        _explosion.Explode();
        _hitBox.gameObject.SetActive(true);
        
        var colliders = Physics.OverlapSphere( transform.position, 10);  
        foreach (var collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(_hitBox.Damage * 10, transform.position, _explosionRadius);
            }
        }
        yield return new WaitForSeconds(0.5f);
        _hitBox.gameObject.SetActive(false);
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
