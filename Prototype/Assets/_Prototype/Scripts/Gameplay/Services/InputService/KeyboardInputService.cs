using UnityEngine;

namespace _Prototype.Scripts.Gameplay.Services.InputService
{
    public class KeyboardInputService : IInputService
    {
        public Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}