
using System;
using _1.Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnZone : MonoBehaviour
{
    IShape shape;
    [SerializeField] GameObject prefabToSpawn;
    public event Action<GameObject> Spawned; 
    void Awake()
    {
        shape = gameObject.GetComponent<IShape>();
        if (shape == null) Debug.LogError("Shape not found");
    }
    public void Spawn()
    {
        var pos = GetRandomPosInBounds();
        var instance = Instantiate(prefabToSpawn, pos, Quaternion.identity);
        Spawned?.Invoke(instance);
    }
    Vector3 GetRandomPosInBounds()
    {
        var x = Random.Range(transform.position.x - shape.Size.x / 2, transform.position.x + shape.Size.x / 2);
        var y = Random.Range(transform.position.y - shape.Size.y / 2, transform.position.y + shape.Size.y / 2);
        var z = Random.Range(transform.position.z - shape.Size.z / 2, transform.position.z + shape.Size.z / 2);
        return new Vector3(x, y, z);
    }
}
