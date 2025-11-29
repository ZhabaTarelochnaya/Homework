using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour
{
    InputActions _inputActions;
    Camera _mainCamera;
    [SerializeField] float maxYRotation = 50f;
    [SerializeField] float minYRotation = -50f;
    [SerializeField] float maxXRotation = 30f;
    [SerializeField] float minXRotation = 0;
    [SerializeField] float maxShootForce = 20f;

    [SerializeField] float shootXRotation;
    // [SerializeField] float 
    [SerializeField] GameObject cannonBallPrefab;

    public void Bind(InputActions inputActions, Camera mainCamera)
    {
        _inputActions = inputActions;
        _mainCamera = mainCamera;
        inputActions.Gameplay.Shoot.canceled += OnShootCancelled;
    }

    void OnShootCancelled(InputAction.CallbackContext obj)
    {
        var ball = Instantiate(cannonBallPrefab, transform.position, Quaternion.identity);
        var ballRigidBody = ball.GetComponent<Rigidbody>();
        var shootDirection = Quaternion.Euler(0, shootXRotation, 0) * transform.forward;
        ballRigidBody.AddForce(shootDirection * maxShootForce, ForceMode.Impulse);
    }

    public void Update()
    {
        if (!_inputActions.Gameplay.enabled) return;
        var mousePos = Mouse.current.position.ReadValue();
        var mouseXPos = mousePos.x / Screen.width;
        var mouseYPos = mousePos.y / Screen.height;
        var cannonXRotation = Mathf.Lerp(minXRotation, maxXRotation, mouseYPos);
        var cannonYRotation = Mathf.Lerp(minYRotation, maxYRotation, mouseXPos);
        transform.rotation = Quaternion.Euler(-cannonXRotation, cannonYRotation, transform.eulerAngles.z);
    }
}
