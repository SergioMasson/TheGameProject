using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PlayAnimationSoundAction", menuName = "State Machines/Actions/Play Animation Sound")]
public class PlayAudioOnAnimationTriggerActionSO : StateActionSO
{
    [SerializeField]
    private string _triggerName = default;

    [SerializeField]
    private AudioCueSO _audioCue = default;

    [SerializeField]
    private AudioCueEventChannelSO _channel = default;

    [SerializeField]
    private AudioConfigurationSO _config = default;

    protected override StateAction CreateAction() => new PlayAudionOnAnimationTriggerAction(_triggerName, _audioCue, _channel, _config);
}

public class PlayAudionOnAnimationTriggerAction : StateAction
{
    private Player _player;

    private readonly string _triggerName;

    private readonly AudioCueSO _audioCue;

    private readonly AudioCueEventChannelSO _channel;

    private readonly AudioConfigurationSO _config;

    public PlayAudionOnAnimationTriggerAction(string triggerName, AudioCueSO audioCue, AudioCueEventChannelSO channel, AudioConfigurationSO config)
    {
        _triggerName = triggerName;
        _audioCue = audioCue;
        _channel = channel;
        _config = config;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _player = stateMachine.GetComponent<Player>();
    }

    public override void OnStateEnter()
    {
        _player.OnAnimationTriggered += PlayerOnAnimationTriggered;
    }

    public override void OnStateExit()
    {
        _player.OnAnimationTriggered -= PlayerOnAnimationTriggered;
    }

    private void PlayerOnAnimationTriggered(string arg0)
    {
        if (_triggerName.Equals(arg0))
            _channel.RaiseEvent(_audioCue, _config, _player.transform.position);
    }

    public override void OnUpdate()
    {
    }
}