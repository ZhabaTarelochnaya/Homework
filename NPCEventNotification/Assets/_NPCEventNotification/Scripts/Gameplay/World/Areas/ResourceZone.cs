
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours.ResourceCollection
{
    public class ResourceZone : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        [field: SerializeField] public float CollectionTime { get; private set; } = 2f;
        public Vector2 Size => _spriteRenderer.size;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (!_spriteRenderer) Debug.LogError($"{_spriteRenderer.name} has no SpriteRenderer");
        }

        public Vector2 GetRandomPosition()
        {
            var x = Random.Range(transform.position.x - Size.x / 2 * transform.localScale.x, 
                transform.position.x + Size.x / 2 * transform.localScale.x);
            var y = Random.Range(transform.position.y - Size.y / 2 * transform.localScale.y,
                transform.position.y + Size.y / 2 * transform.localScale.y);
            return new Vector2(x, y);
        }
    }
}