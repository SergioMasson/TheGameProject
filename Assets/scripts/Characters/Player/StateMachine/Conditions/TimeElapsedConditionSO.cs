using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeElapsed", menuName = "State Machines/Conditions/Time elapsed")]
public class TimeElapsedConditionSO : StateConditionSO
{
    [SerializeField] private float _timerLength = .5f;

    protected override Condition CreateCondition() => new TimeElapsedCondition(_timerLength);
}

/// <summary>
/// Condition to check if time has passed since time.
/// </summary>
public class TimeElapsedCondition : Condition
{
    private float _timerLength;
    private float _startTime;

    public override void OnStateEnter()
    {
        _startTime = Time.time;
    }

    public TimeElapsedCondition(float timerLength)
    {
        _timerLength = timerLength;
    }

    protected override bool Statement() => Time.time >= _startTime + _timerLength;
}