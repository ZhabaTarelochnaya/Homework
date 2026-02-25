using _MultiplayerFPS.Scripts.Controllers;
using _MultiplayerFPS.Scripts.Services.InputService;
using _MultiplayerFPS.Scripts.Services.LoggerService;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts
{
    public class GameNetworkPlayerView : NetworkBehaviour
    {
        static readonly int MoveHorizontal = Animator.StringToHash("MoveHorizontal");
        static readonly int MoveVertical = Animator.StringToHash("MoveVertical");
        
        [SyncVar(hook = nameof(OnNicknameChanged)), HideInInspector] 
        public string Nickname;
        [SyncVar(hook = nameof(OnColorChanged)), HideInInspector]  
        public Color Color;
        
        PlayerController _playerController;
        IInputService _inputService;
        ILoggerService _loggerService;
        NicknameTagView _nicknameTagView;
        [SerializeField] NicknameTagView _nicknameTagViewPrefab;
        [SerializeField] Renderer _renderer;
        [SerializeField] Animator _animator;
        [SerializeField] Transform _headTransform;

        public override void OnStartClient()
        {
            _nicknameTagView = Instantiate(_nicknameTagViewPrefab, transform);
            OnNicknameChanged("", Nickname);
            OnColorChanged(Color.black, Color);
            _loggerService = ServiceLocator.Current.Get<ILoggerService>();
            _loggerService.Log($"Player {Nickname} (netId: {netId}) joined the game.");
        }

        public override void OnStopClient()
        {
            _loggerService.Log($"Player {Nickname} (netId: {netId}) left the game.");
        }
        public void Init(PlayerController playerController)
        {
            _playerController = playerController;
            _inputService = ServiceLocator.Current.Get<IInputService>();
        }
        
        void Update()
        {
            if (isLocalPlayer)
            {
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