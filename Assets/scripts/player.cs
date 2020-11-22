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

    [HideInInspector]
    public bool jumpInput;

    [HideInInspector]
    public Vector3 movementInput; //Initial input coming from the Protagonist script

    [HideInInspector]
    public Vector3 movementVector; //Final movement vector, manipulated by the StateMachine actions

    // Start is called before the first frame update
    private void OnEnable()
    {
        _inputReader.moveEvent += OnMove;
        _inputReader.jumpEvent += OnJumpCanceled;
        _inputReader.jumpCanceledEvent += OnJumpStarted;
    }

    private void OnJumpStarted()
    {
        jumpInput = false;
    }

    private void OnJumpCanceled()
    {
        jumpInput = true;
    }

    private void OnMove(Vector2 arg0)
    {
        movementInput = new Vector3(arg0.x, 0, arg0.y);
    }
}