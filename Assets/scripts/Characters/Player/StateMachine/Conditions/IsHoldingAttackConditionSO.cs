using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsHoldingAttack", menuName = "State Machines/Conditions/Is Holding Attack")]
public class IsHoldingAttackConditionSO : StateConditionSO<IsHoldingAttackCondition> { }

public class IsHoldingAttackCondition : Condition
{
    //Component references
    private Player _protagonistScript;

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Player>();
    }

    protected override bool Statement() => _protagonistScript.dashInput;
}