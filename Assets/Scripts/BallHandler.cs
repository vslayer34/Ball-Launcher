using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Vector2 _touchPosition;


    private void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            _touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Debug.Log(_touchPosition);
        }
    }
}
