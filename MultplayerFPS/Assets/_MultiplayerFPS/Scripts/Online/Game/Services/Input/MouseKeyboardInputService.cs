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
        public bool GetJumpButtonDown() => Input.GetButtonDown("Jump");
        public Vector2 GetLook()
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");
            return new Vector2(x, y);
        }
        public bool GetShootButtonDown() => Input.GetButtonDown("Fire1");
        public bool GetShootButton() => Input.GetButton("Fire1");
        public bool GetHealButtonDown() => Input.GetKeyDown(KeyCode.H);
        public bool GetGrenadeButtonDown() => Input.GetKeyDown(KeyCode.G);
        public bool GetLeaderboardButtonDown() => Input.GetKeyDown(KeyCode.Tab);

        float Sign(float value)
        {
            if (value < 0) return -1;
            if (value > 0) return 1;
            return 0;
        }
        
    }
}