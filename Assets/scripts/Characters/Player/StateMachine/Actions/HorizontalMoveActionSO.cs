using UnityEngine;
using TheGame.StateMachine;
using TheGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HorizontalMove", menuName = "State Machines/Actions/Horizontal Move")]
public class HorizontalMoveActionSO : StateActionSO
{
    [Tooltip("Horizontal XZ plane speed multiplier")] [SerializeField] private float _speed = 8f;

    protected override StateAction CreateAction() => new WalkAction(_speed);
}

public class WalkAction : StateAction
{
    //Component references
    private Player _protagonistScript;

    private Camera _mainCamera;

    private float _speed;

    public WalkAction(float speed)
    {
        _speed = speed;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _protagonistScript = stateMachine.GetComponent<Player>();
        _mainCamera = Camera.main;
    }

    public override void OnUpdate()
    {
        var transformedPositon = (_mainCamera.transform.rotation * _protagonistScript.movementInput);

        transformedPositon.Normalize();

        _protagonistScript.movementVector.x = transformedPositon.x * _speed;
        _protagonistScript.movementVector.z = transformedPositon.z * _speed;
    }
}