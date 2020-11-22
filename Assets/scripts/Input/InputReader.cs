using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, MainInput.IGameplayActions
{
    // Gameplay
    public event UnityAction jumpEvent;

    public event UnityAction jumpCanceledEvent;

    public event UnityAction attackEvent;

    public event UnityAction focusEvent; // Used to talk, pickup objects, interact with tools like the cooking cauldron

    public event UnityAction pauseEvent;

    public event UnityAction<Vector2> moveEvent;

    public event UnityAction<Vector2, bool> cameraMoveEvent;

    public event UnityAction enableMouseControlCameraEvent;

    public event UnityAction disableMouseControlCameraEvent;

    private MainInput gameInput;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new MainInput();
            gameInput.gameplay.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void EnableGameplayInput()
    {
        gameInput.gameplay.Enable();
    }

    public void DisableAllInput()
    {
        gameInput.gameplay.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (moveEvent != null)
            moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumpEvent != null && context.phase == InputActionPhase.Performed)
            jumpEvent.Invoke();

        if (jumpCanceledEvent != null && context.phase == InputActionPhase.Canceled)
            jumpCanceledEvent.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (attackEvent != null && context.phase == InputActionPhase.Performed)
            attackEvent.Invoke();
    }

    public void OnFocus(InputAction.CallbackContext context)
    {
        if (focusEvent != null && context.phase == InputActionPhase.Performed)
            focusEvent.Invoke();
    }

    public void OnCameraMove(InputAction.CallbackContext context)
    {
        if (cameraMoveEvent != null)
            cameraMoveEvent.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    public void OnMouseControlCamera(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            enableMouseControlCameraEvent?.Invoke();

        if (context.phase == InputActionPhase.Canceled)
            disableMouseControlCameraEvent?.Invoke();
    }

    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
}