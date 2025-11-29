using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [field: SerializeField] public int LifeTime { get; set; } = 3;

    IEnumerator Start()
    {
        Debug.Log("Target created");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < LifeTime - 1; i++)
        {
            Debug.Log("Target still alive");
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Target destroyed");
        Destroy(gameObject);
    }
}
