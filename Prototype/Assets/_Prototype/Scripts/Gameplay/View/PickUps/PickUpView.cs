using _Prototype.Scripts.Gameplay.Services.PickUpService;
using UnityEngine;

namespace _Prototype.Scripts.Gameplay.View
{
    public class PickUpView : MonoBehaviour, IPickUpView
    {
        public int ID { get; set; }
        public void Collect()
        {
            Destroy(gameObject);
        }
    }
}