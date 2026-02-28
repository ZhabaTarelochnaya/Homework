using System;
using TMPro;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class NicknameTagView : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        [SerializeField] FillableBar _fillBar;
        void Update()
        {
            Vector3 direction = Camera.main.transform.position - transform.position;
            direction.y = 0; 
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
        public void SetColor(Color color) => _text.color = color;
        public void SetNickname(string nickname) => _text.text = nickname;
        public void SetHealth(float health) => _fillBar.FillAmount = health;
    }
}