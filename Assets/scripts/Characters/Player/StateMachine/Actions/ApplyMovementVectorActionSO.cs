using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyMovementVector", menuName = "State Machines/Actions/Apply Movement Vector")]
public class ApplyMovementVectorActionSO : StateActionSO<ApplyMovementVectorAction> { }

public class ApplyMovementVectorAction : StateAction
{
    //Component references
    private Player _protagonistScript;

    private CharacterController _characterController;

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Player>();
        _characterController = stateMachine.GetComponent<CharacterController>();
    }

    public override void OnUpdate()
    {
        _characterController.Move(_protagonistScript.movementVector * Time.deltaTime);
    }
}