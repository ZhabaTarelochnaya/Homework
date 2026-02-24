using System;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameNetworkPlayer : NetworkBehaviour
    {
        static readonly int MoveHorizontal = Animator.StringToHash("MoveHorizontal");
        static readonly int MoveVertical = Animator.StringToHash("MoveVertical");

        [SyncVar(hook = nameof(OnNicknameChanged)), HideInInspector] 
        public string Nickname;
        [SyncVar(hook = nameof(OnColorChanged)), HideInInspector]  
        public Color Color;
   
        NicknameTagView _nicknameTagView;
        IInputService _inputService;
        [SerializeField] NicknameTagView _nicknameTagViewPrefab;
        [SerializeField] Renderer _renderer;
        [SerializeField] CharacterController _characterController;
        [SerializeField] Animator _animator;

        public override void OnStartClient()
        {
            _nicknameTagView = Instantiate(_nicknameTagViewPrefab, transform);
            OnNicknameChanged("", Nickname);
            OnColorChanged(Color.black, Color);
            _inputService = ServiceLocator.Current.Get<IInputService>();
        }
        void Update()
        {
            if (isLocalPlayer)
            {
                var move = _inputService.GetMove();
                Vector3 move3 = new Vector3(move.x, 0, move.y) * Time.deltaTime * 10f;
                _characterController.Move(move3);
                _animator.SetInteger(MoveVertical, (int)move.y);
                _animator.SetInteger(MoveHorizontal, (int)move.x);
                CmdPlayMoveAnimation(move);
                CmdMovePLayer(move * Time.deltaTime * 10f);
            }
        }

        [Command]
        void CmdPlayMoveAnimation(Vector2 move)
        {
            _animator.SetInteger(MoveVertical, (int)move.y);
            _animator.SetInteger(MoveHorizontal, (int)move.x);
        }

        [Command]
        void CmdMovePLayer(Vector2 move)
        {
            var move3 = new Vector3(move.x, 0, move.y);
            _characterController.Move(move3);
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
