using System;
using System.Collections;
using _1.Gameplay.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    GameplayDataProxy _gameplayDataProxy;
    [field: SerializeField] public int LifeTime { get; set; } = 3;
    [field: SerializeField] public float ScaleAnimationSpeed { get; set; } = 2f; 
    [field: SerializeField] public float RotateAnimationSpeed { get; set; } = 1f;

    public void Bind(GameplayDataProxy gameplayDataProxy)
    {
        _gameplayDataProxy = gameplayDataProxy;
    }
    void OnTriggerEnter(Collider other)
    {
        _gameplayDataProxy.TargetsHit.Value += 1;
        Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("Target created");
        StartCoroutine(LateDestroy());
        var animationNumber = Random.Range(0, 2);
        if (animationNumber == 0)
        {
            StartCoroutine(ScaleAnimation());
        }
        else if (animationNumber == 1)
        {
            StartCoroutine(RotateAnimation());
        }
        
    }
    IEnumerator LateDestroy()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < LifeTime - 1; i++)
        {
            Debug.Log("Target still alive");
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
    IEnumerator ScaleAnimation()
    {
        while (true)
        {
            while (true)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * 2,
                    Time.deltaTime * ScaleAnimationSpeed);
                if (transform.localScale.x < 2) yield return null;
                else break;
            }
            while (true)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one,
                    Time.deltaTime * ScaleAnimationSpeed);
                if (transform.localScale.x > 1) yield return null;
                else break;
            }
        }
    }
    IEnumerator RotateAnimation()
    {
        while (true)
        {
            transform.Rotate(Vector3.up, RotateAnimationSpeed * 360 * Time.deltaTime);
            yield return null;
        }
    }
    void OnDestroy() => Debug.Log("Target destroyed");
}
