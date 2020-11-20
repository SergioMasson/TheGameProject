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

    public event UnityAction<Vector2, bool> moveEvent;

    public event UnityAction<Vector2, bool> cameraMoveEvent;

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
        {
            if (context.phase == InputActionPhase.Canceled)
                moveEvent.Invoke(new Vector2(0, 0), false);

            moveEvent.Invoke(context.ReadValue<Vector2>(), true);
        }
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
        {
            if (context.phase == InputActionPhase.Canceled)
                cameraMoveEvent.Invoke(new Vector2(0, 0), false);
            else
                cameraMoveEvent.Invoke(context.ReadValue<Vector2>(), true);
        }
    }
}