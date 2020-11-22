using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "StartedMoving", menuName = "State Machines/Conditions/Started Moving")]
public class IsMovingConditionSO : StateConditionSO
{
    [SerializeField]
    private float _treshold = 0.02f;

    protected override Condition CreateCondition() => new IsMovingCondition(_treshold);
}

/// <summary>
/// Condition responsible for checking if the player is moving.
/// </summary>
public class IsMovingCondition : Condition
{
    private float _threshold;
    private Player _protagonistScript;

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Player>();
    }

    public IsMovingCondition(float treshold)
    {
        _threshold = treshold;
    }

    /// <summary>
    /// Checks if the player current input vector is greater than a treshold value.
    /// </summary>
    /// <returns></returns>
    protected override bool Statement()
    {
        Vector3 movementVector = _protagonistScript.movementInput;
        movementVector.y = 0f;
        return movementVector.sqrMagnitude > _threshold;
    }

    public override void OnStateExit()
    {
        _protagonistScript.movementVector = Vector3.zero;
    }
}