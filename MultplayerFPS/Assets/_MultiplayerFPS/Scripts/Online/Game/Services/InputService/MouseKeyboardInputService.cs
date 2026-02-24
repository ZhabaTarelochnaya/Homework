using UnityEngine;

namespace _MultiplayerFPS.Scripts.Services.InputService
{
    public class MouseKeyboardInputService : IInputService
    {
        public Vector2 GetMove()
        {
            var x = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
            var y = Mathf.Sign(Input.GetAxisRaw("Vertical"));
            return new Vector2(x, y);
        }
    }
}