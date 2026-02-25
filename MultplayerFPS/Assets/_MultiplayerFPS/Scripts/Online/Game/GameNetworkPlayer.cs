using System;
using _MultiplayerFPS.Scripts.Controllers;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.Move;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    [DefaultExecutionOrder(-100)]
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
        PlayerController _playerController;
        [SerializeField] NicknameTagView _nicknameTagViewPrefab;
        [SerializeField] Renderer _renderer;
        [SerializeField] CharacterController _characterController;
        [SerializeField] Animator _animator;
        [SerializeField] Transform _headTransform;
        public override void OnStartClient()
        {
            _nicknameTagView = Instantiate(_nicknameTagViewPrefab, transform);
            OnNicknameChanged("", Nickname);
            OnColorChanged(Color.black, Color);
        }

        public override void OnStartLocalPlayer()
        {
            var inputService = new MouseKeyboardInputService();
            ServiceLocator.Current.Register<IInputService>(inputService);
            var characterController = NetworkClient.localPlayer.GetComponent<CharacterController>();
            var moveService = new CharacterControllerPlayerMovementService(characterController);
            ServiceLocator.Current.Register<IPlayerMovementService>(moveService);
            
            _playerController = new PlayerController();
            _inputService = ServiceLocator.Current.Get<IInputService>();
        }

        void Update()
        {
            if (isLocalPlayer)
            {
                _playerController.HandleMove();
                _playerController.HandleJump();
                _playerController.HandlePlayerRotation(transform);
                HandleAnimations();
            }
        }

        void LateUpdate()
        {
            if (isLocalPlayer)
            {
                _playerController.HandleCameraRotation(_headTransform, transform);
            }
        }

        void HandleAnimations()
        {
            var move = _inputService.GetMove();
            _animator.SetInteger(MoveVertical, (int)move.y);
            _animator.SetInteger(MoveHorizontal, (int)move.x);
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
