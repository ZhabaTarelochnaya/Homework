using System;
using TMPro;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class NicknameTagView : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        void Update()
        {
            Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
        
        public void SetColor(Color color) => _text.color = color;
        public void SetNickname(string nickname) => _text.text = nickname;
    }
}