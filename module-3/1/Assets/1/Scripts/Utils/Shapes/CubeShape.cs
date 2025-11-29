using _1.Gameplay;
using UnityEngine;
[ExecuteAlways]
public class CubeShape : MonoBehaviour, IShape
{
    [field: SerializeField] public Vector3 Size { get; set; } = new Vector3(5f, 5f, 5f);
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Size);
    }
}
