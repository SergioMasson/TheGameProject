using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

using Moment = TheGame.StateMachine.StateAction.SpecificMoment;

[CreateAssetMenu(fileName = "PlayAudioOnStateTransitionAction", menuName = "State Machines/Actions/Play Sound on State change")]
public class PlayAudioOnStateTransitionActionSO : StateActionSO
{
    [SerializeField]
    private Moment _whenToRun = default;

    [SerializeField]
    private AudioCueSO _audioCue = default;

    [SerializeField]
    private AudioCueEventChannelSO _channel = default;

    [SerializeField]
    private AudioConfigurationSO _config = default;

    protected override StateAction CreateAction() => new PlayAudioOnStateTransitionAction(_whenToRun, _audioCue, _channel, _config);
}

public class PlayAudioOnStateTransitionAction : StateAction
{
    private readonly Moment _whenToRun;

    private readonly AudioCueSO _audioCue;

    private readonly AudioCueEventChannelSO _channel;

    private readonly AudioConfigurationSO _config;

    private Transform _targetTransform;

    public PlayAudioOnStateTransitionAction(Moment whenToRun, AudioCueSO audioCue, AudioCueEventChannelSO channel, AudioConfigurationSO config)
    {
        _whenToRun = whenToRun;
        _audioCue = audioCue;
        _channel = channel;
        _config = config;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _targetTransform = stateMachine.transform;
    }

    public override void OnStateEnter()
    {
        if (_whenToRun == Moment.OnStateEnter)
            PlayAudio();
    }

    public override void OnStateExit()
    {
        if (_whenToRun == Moment.OnStateExit)
            PlayAudio();
    }

    private void PlayAudio()
    {
        _channel.RaiseEvent(_audioCue, _config, _targetTransform.position);
    }

    public override void OnUpdate()
    {
    }
}