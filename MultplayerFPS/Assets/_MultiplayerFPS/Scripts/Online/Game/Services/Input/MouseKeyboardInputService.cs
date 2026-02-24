using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.InputService
{
    public class MouseKeyboardInputService : IInputService
    {
        public Vector2 GetMove()
        {
            var x = Sign(Input.GetAxisRaw("Horizontal"));
            var y = Sign(Input.GetAxisRaw("Vertical"));
            return new Vector2(x, y);
        }
        public bool JumpButtonDown() => Input.GetButtonDown("Jump");

        float Sign(float value)
        {
            if (value < 0) return -1;
            if (value > 0) return 1;
            return 0;
        }
        
    }
}