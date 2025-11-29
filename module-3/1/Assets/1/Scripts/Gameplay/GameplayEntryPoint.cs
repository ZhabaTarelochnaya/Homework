using UnityEngine;

namespace _1.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] SpawnZone _spawnZone;
        public void Bind()
        {
            var gameManager = new GameManager(_spawnZone);
            
            StartCoroutine(gameManager.Run());
        }
    }
}