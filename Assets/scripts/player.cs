using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private Transform _cameraTransform;

    [SerializeField]
    private Cinemachine.CinemachineFreeLook _freeLookCamera;

    [SerializeField]
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 2;

    [SerializeField]
    private float turnSmoothTime = 0.1f;

    private float turnSomoothVelocity;

    private Vector2 _cameraMovement = new Vector2(0, 0);

    private Vector3 _playerMovement = new Vector3(0, 0, 0);

    private bool _isRotating = false;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _inputReader.cameraMoveEvent += cameraMoveEvent;
        _inputReader.moveEvent += moveEvent;
    }

    private void moveEvent(Vector2 arg0, bool isMoving)
    {
        Debug.Log($"Move event {arg0}");
        _playerMovement = new Vector3(arg0.x, 0, arg0.y);
    }

    private void cameraMoveEvent(Vector2 arg0, bool isRotating)
    {
        _cameraMovement = arg0;
        _isRotating = isRotating;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isRotating)
        {
            _freeLookCamera.m_XAxis.Value += _cameraMovement.x * Time.deltaTime * 180;
            _freeLookCamera.m_YAxis.Value += _cameraMovement.y * Time.deltaTime;
        }

        if (_playerMovement.sqrMagnitude >= 0.1f)
        {
            var cameraSpaceDir = (_cameraTransform.rotation * _playerMovement);
            cameraSpaceDir.y = 0;
            cameraSpaceDir.Normalize();

            float targetAngle = Mathf.Atan2(cameraSpaceDir.x, cameraSpaceDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSomoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            _controller.Move(cameraSpaceDir * _speed * Time.deltaTime);
        }
    }
}