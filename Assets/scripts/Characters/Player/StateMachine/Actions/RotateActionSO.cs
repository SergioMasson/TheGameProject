﻿using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "RotateAction", menuName = "State Machines/Actions/Rotate")]
public class RotateActionSO : StateActionSO
{
    [Tooltip("Smoothing for rotating the character to their movement direction")] [SerializeField] private float _turnSmoothTime = 0.2f;

    protected override StateAction CreateAction() => new RotateAction(_turnSmoothTime);
}

public class RotateAction : StateAction
{
    //Component references
    private Player _protagonistScript;

    private Transform _transform;

    private float _turnSmoothTime;
    private float _turnSmoothSpeed; //Used by Mathf.SmoothDampAngle to smoothly rotate the character to their movement direction
    private const float ROTATION_TRESHOLD = .02f; // Used to prevent NaN result causing rotation in a non direction

    public RotateAction(float turnSmoothTime)
    {
        _turnSmoothTime = turnSmoothTime;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Player>();
        _transform = stateMachine.GetComponent<Transform>();
    }

    public override void OnUpdate()
    {
        Vector3 horizontalMovement = _protagonistScript.movementVector;
        horizontalMovement.y = 0f;

        if (horizontalMovement.sqrMagnitude >= ROTATION_TRESHOLD)
        {
            float targetRotation = Mathf.Atan2(_protagonistScript.movementVector.x, _protagonistScript.movementVector.z) * Mathf.Rad2Deg;
            _transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(
                _transform.eulerAngles.y,
                targetRotation,
                ref _turnSmoothSpeed,
                _turnSmoothTime);
        }
    }
}