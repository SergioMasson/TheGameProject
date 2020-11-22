using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

using Moment = TheGame.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "ApplyExplosionEffect", menuName = "State Machines/Actions/Apply Explosion Effect")]
public class ExplosionActionSO : StateActionSO
{
    [SerializeField]
    private Moment _whenToRun = default; // Allows this StateActionSO type to be reused for all 3 state moments.

    protected override StateAction CreateAction() => new ExplosionAction(_whenToRun);
}

public class ExplosionAction : StateAction
{
    //Component references
    private Explosion _explosion;

    private readonly Moment _whenToRun;

    public ExplosionAction(Moment whenToRun)
    {
        _whenToRun = whenToRun;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _explosion = stateMachine.GetComponent<Player>().Explostion;
    }

    public override void OnStateEnter()
    {
        if (_whenToRun == Moment.OnStateEnter)
            _explosion.Play();
    }

    public override void OnStateExit()
    {
        if (_whenToRun == Moment.OnStateExit)
            _explosion.Play();
    }

    public override void OnUpdate()
    {
    }
}