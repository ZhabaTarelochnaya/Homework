using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameNetworkPlayer : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnNicknameChanged)), HideInInspector] 
        public string Nickname;
        [SyncVar(hook = nameof(OnColorChanged)), HideInInspector]  
        public Color Color;
   
        NicknameTagView _nicknameTagView;
        [SerializeField] NicknameTagView _nicknameTagViewPrefab;
        [SerializeField] Renderer _renderer;

        public override void OnStartClient()
        {
            _nicknameTagView = Instantiate(_nicknameTagViewPrefab, transform);
            _renderer = GetComponent<Renderer>();
            OnNicknameChanged("", Nickname);
            OnColorChanged(Color.black, Color);
        }

        void OnNicknameChanged(string oldNickname, string newNickname)
        {
            if (_nicknameTagView == null) return;
            _nicknameTagView?.SetNickname(newNickname);
        }

        void OnColorChanged(Color oldColor, Color newColor)
        {
            if (_nicknameTagView == null) return;
            _nicknameTagView.SetColor(newColor);
            _renderer.material.color = newColor;
        }
    }
}
