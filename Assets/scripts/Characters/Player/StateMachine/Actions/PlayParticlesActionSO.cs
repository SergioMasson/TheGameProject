using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

using Moment = TheGame.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "PlayParticlesAction", menuName = "State Machines/Actions/Play Particles Effect")]
public class PlayParticlesActionSO : StateActionSO
{
    [SerializeField]
    private Moment _whenToRun = default; // Allows this StateActionSO type to be reused for all 3 state moments.

    [SerializeField]
    private string _componentName = default;

    protected override StateAction CreateAction() => new PlayParticlesAction(_whenToRun, _componentName);
}

public class PlayParticlesAction : StateAction
{
    private ParticleSystem _targetParicleSystem;

    private readonly Moment _whenToRun;

    private readonly string _componentName;

    public PlayParticlesAction(Moment whenToRun, string componentName)
    {
        _whenToRun = whenToRun;
        _componentName = componentName;
    }

    public override void Awake(StateMachine stateMachine)
    {
        if (_componentName == null)
            _targetParicleSystem = stateMachine.GetComponent<ParticleSystem>();
        else
        {
            var targetGO = stateMachine.gameObject.transform.Find(_componentName);
            _targetParicleSystem = targetGO.GetComponent<ParticleSystem>();
        }
    }

    public override void OnStateEnter()
    {
        if (_whenToRun == Moment.OnStateEnter || _whenToRun == Moment.OnUpdate)
            _targetParicleSystem.Play();
    }

    public override void OnStateExit()
    {
        if (_whenToRun == Moment.OnStateExit)
            _targetParicleSystem.Play();
        else if (_whenToRun == Moment.OnUpdate)
            _targetParicleSystem.Stop();
    }

    public override void OnUpdate()
    {
    }
}