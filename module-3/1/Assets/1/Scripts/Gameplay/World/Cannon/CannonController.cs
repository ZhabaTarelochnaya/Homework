using System.Collections;
using System.Collections.Generic;
using _1.Gameplay.World.Cannon;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour
{
    InputActions _inputActions;
    float _timer;
    Camera _mainCamera;
    [SerializeField] float _maxYRotation = 50f;
    [SerializeField] float _minYRotation = -50f;
    [SerializeField] float _maxXRotation = 30f;
    [SerializeField] float _minXRotation = 0;
    [SerializeField] CannonStats _cannonStats;
    [SerializeField] GameObject _cannonBallPrefab;

    public void Bind(InputActions inputActions, Camera mainCamera)
    {
        _inputActions = inputActions;
        _mainCamera = mainCamera;
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
        if (_timer > 0) return;
        _timer = _cannonStats.ShootCoolDown;
        var ball = Instantiate(_cannonBallPrefab, transform.position, Quaternion.identity);
        var ballRigidBody = ball.GetComponent<Rigidbody>();
        ballRigidBody.AddForce(transform.forward * _cannonStats.ProjectileSpeed, ForceMode.Impulse);
    }

    void OnDestroy()
    {
        if (_inputActions == null) return;
        _inputActions.Gameplay.Shoot.started -= OnShootStarted;
    }
}
