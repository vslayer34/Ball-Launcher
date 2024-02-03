using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector2 _touchPosition;
    private Vector3 _worldTouchPosition;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            _touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            _worldTouchPosition = _mainCamera.ScreenToWorldPoint(_touchPosition);
            _worldTouchPosition.z = 0.0f;
            
            Debug.Log(_worldTouchPosition);
        }
    }
}
