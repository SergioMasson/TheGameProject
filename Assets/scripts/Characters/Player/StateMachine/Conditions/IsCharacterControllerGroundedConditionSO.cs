using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsGrounded", menuName = "State Machines/Conditions/Is Character Controller Grounded")]
public class IsCharacterControllerGroundedConditionSO : StateConditionSO<IsCharacterControllerGroundedCondition> { }

/// <summary>
/// Checks if a character is grounded.
/// </summary>
public class IsCharacterControllerGroundedCondition : Condition
{
    private CharacterController _characterController;

    public override void Awake(StateMachine stateMachine)
    {
        _characterController = stateMachine.GetComponent<CharacterController>();
    }

    protected override bool Statement() => _characterController.isGrounded;
}