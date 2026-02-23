using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    [RequireComponent(typeof(Renderer))]
    public class GameNetworkPlayer : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnNicknameChanged)), HideInInspector] 
        public string Nickname;
        [SyncVar(hook = nameof(OnColorChanged)), HideInInspector]  
        public Color Color;
        
        Renderer _renderer;
        NicknameTagView _nicknameTagView;
        [SerializeField] NicknameTagView _nicknameTagViewPrefab;

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
