using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    // reference for the baulder prefab and pivot point
    [SerializeField, Tooltip("Reference to the bauder prefab")]
    private GameObject _baulderPrefab;

    [SerializeField, Tooltip("Reference to the pivot point")]
    private Rigidbody2D _jointPivot;




    [SerializeField, Tooltip("Spawn delay for a new baulder")]
    private float _spawnDelay;


    // camera and touch screen & world positions
    private Camera _mainCamera;
    private Vector2 _touchPosition;
    private Vector3 _worldTouchPosition;


    // references to the baulder rigidbody
    private Rigidbody2D _baulderRb;
    private Joint2D _baulderJoint;


    [SerializeField, Tooltip("When to release the bauder from the joint after launching")]
    private float _releaseBaulderAfter;

    private bool _isDraggingBall;

    private void Start()
    {
        _mainCamera = Camera.main;

        SpawnNewBaulder();
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

        Invoke(nameof(SpawnNewBaulder), _spawnDelay);
    }


    private void SpawnNewBaulder()
    {
        GameObject newBaulder = Instantiate(_baulderPrefab, _jointPivot.transform.position, Quaternion.identity);

        _baulderRb = newBaulder.GetComponent<Rigidbody2D>();
        _baulderJoint = newBaulder.GetComponent<Joint2D>();

        _baulderJoint.connectedBody = _jointPivot;
    }
}
