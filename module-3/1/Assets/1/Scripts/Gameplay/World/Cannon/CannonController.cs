using System;
using System.Collections;
using System.Collections.Generic;
using _1.Gameplay.Data;
using _1.Gameplay.World.Cannon;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour
{
    GameplayDataProxy _gameplayDataProxy;
    InputActions _inputActions;
    float _timer;
    Animation _animation;
    [SerializeField] float _maxYRotation = 50f;
    [SerializeField] float _minYRotation = -50f;
    [SerializeField] float _maxXRotation = 30f;
    [SerializeField] float _minXRotation = 0;
    [SerializeField] CannonStats _cannonStats;
    [SerializeField] GameObject _cannonBallPrefab;

    void Awake()
    {
        if (_cannonStats == null)
        {
            Debug.LogError("CannonStats not found");
        }
        _animation = GetComponent<Animation>();
    }

    public void Bind(InputActions inputActions, GameplayDataProxy gameplayDataProxy)
    {
        _inputActions = inputActions;
        _gameplayDataProxy = gameplayDataProxy;
        inputActions.Gameplay.Shoot.started += OnShootStarted;
    }
    public void Update()
    {
        if (!_inputActions.Gameplay.enabled) return;
        
        if (_timer > 0) _timer -= Time.deltaTime;
        
        var mousePos = Mouse.current.position.ReadValue();
        var mouseXPos = mousePos.x / Screen.width;
        var mouseYPos = mousePos.y / Screen.height;
        var cannonXRotation = Mathf.Lerp(_minXRotation, _maxXRotation, mouseYPos);
        var cannonYRotation = Mathf.Lerp(_minYRotation, _maxYRotation, mouseXPos);
        transform.rotation = Quaternion.Euler(-cannonXRotation, cannonYRotation, transform.eulerAngles.z);
    }
    void OnShootStarted(InputAction.CallbackContext obj)
    {
        if (_timer > 0)
        {
            Debug.LogWarning("Cannon is on cooldown");
            return;
        }
        _timer = _cannonStats.ShootCoolDown;
        
        _gameplayDataProxy.ShotsFired.Value += 1;
        var ball = Instantiate(_cannonBallPrefab, transform.position, Quaternion.identity);
        var ballRigidBody = ball.GetComponent<Rigidbody>();
        ballRigidBody.AddForce(transform.forward * _cannonStats.ProjectileSpeed, ForceMode.Impulse);

        _animation.Play("Recoil");
    }

    void OnDestroy()
    {
        if (_inputActions == null) return;
        _inputActions.Gameplay.Shoot.started -= OnShootStarted;
    }
}
