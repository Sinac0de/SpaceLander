using System;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public event EventHandler OnPausePerformed;

    public static InputManager Instance { get; private set; }


    private InputActions inputActions;

    private void Awake() {
        Instance = this;

        inputActions = new InputActions();
        inputActions.Enable();


        inputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPausePerformed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsUpInputPressed() {
        return inputActions.Player.LanderUp.IsPressed();
    }

    public bool IsLeftInputPressed() {
        return inputActions.Player.LanderLeft.IsPressed();
    }

    public bool IsRightInputPressed() {
        return inputActions.Player.LanderRight.IsPressed();
    }

    public Vector2 GetMovementInputVector2D() {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }


    private void OnDestroy() {
        inputActions.Disable();
    }
}
