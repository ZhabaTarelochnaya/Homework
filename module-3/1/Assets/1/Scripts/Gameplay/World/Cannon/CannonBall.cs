using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    const float LifeTime = 5f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Target hit!");
        Destroy(gameObject);
    }
}
