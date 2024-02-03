using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    // camera and touch screen & world positions
    private Camera _mainCamera;
    private Vector2 _touchPosition;
    private Vector3 _worldTouchPosition;


    // references to the baulder and joint
    [SerializeField, Tooltip("Reference to the bauder rigid body")]
    private Rigidbody2D _baulderRb;

    [SerializeField, Tooltip("Reference to the joint to disable it after throwing the baulder")]
    private Joint2D _baulderJoint;

    [SerializeField, Tooltip("When to release the bauder from the joint after launching")]
    private float _releaseBaulderAfter;

    private bool _isDraggingBall;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_baulderRb == null)
        {
            return;
        }


        // Move the baulder with the touch position
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            _touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            
            _worldTouchPosition = _mainCamera.ScreenToWorldPoint(_touchPosition);
            _worldTouchPosition.z = 0.0f;


            _isDraggingBall = true;
            _baulderRb.isKinematic = true;
            _baulderRb.position = _worldTouchPosition;
            
            Debug.Log(_worldTouchPosition);
        }
        else
        {
            if (_isDraggingBall)
            {
                LaunchBaulder();
            }
            
            _isDraggingBall = false;
        }
    }


    /// <summary>
    /// Lanuch the baulder according to the force accumlated in the spring joint
    /// </summary>
    private void LaunchBaulder()
    {
        _baulderRb.isKinematic = false;
        _baulderRb = null;

        Invoke("DeattachBaulder", _releaseBaulderAfter);
    }


    /// <summary>
    /// Dettach the baulder from the joint
    /// </summary>
    private void DeattachBaulder()
    {
        _baulderJoint.enabled = false;
        _baulderJoint = null;
    }
}
