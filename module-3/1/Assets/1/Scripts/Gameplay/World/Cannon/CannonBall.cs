using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    bool didHit;
    const float LifeTime = 5f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Target hit!");
        didHit = true;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!didHit)
        {
            Debug.Log("Shot missed!");
            didHit = true;
        }
    }
}
