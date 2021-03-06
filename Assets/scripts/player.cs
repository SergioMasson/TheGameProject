﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public bool dashInput;

    [HideInInspector]
    public Vector3 movementInput; //Initial input coming from the Protagonist script

    [HideInInspector]
    public Vector3 movementVector; //Final movement vector, manipulated by the StateMachine actions

    [SerializeField]
    public Explosion Explostion;

    public event UnityAction<string> OnAnimationTriggered;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _inputReader.attackEvent += OnAttackEvent;
        _inputReader.attackCanceledEvent += OnAttackCanceled;
        _inputReader.moveEvent += OnMove;
        _inputReader.jumpEvent += OnJumpCanceled;
        _inputReader.jumpCanceledEvent += OnJumpStarted;
    }

    private void OnAttackCanceled()
    {
        dashInput = false;
    }

    private void OnAttackEvent()
    {
        dashInput = true;
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

    public void WalkAnimationTrigger(string trigger)
    {
        OnAnimationTriggered?.Invoke(trigger);
    }
}