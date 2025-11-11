using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private DefaultInputActions _inputActions;

    public Action JumpPerformed, FirePerformed, PausePerformed, ResumePerformed;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        _inputActions = new DefaultInputActions();
        _inputActions.Player.Enable();
    }

    private void Start()
    {
        _inputActions.Player.Jump.performed += OnJumpPerformed;
        _inputActions.Player.Fire.performed += OnFirePerformed;
        _inputActions.Player.Pause.performed += OnPausePerformed;
        _inputActions.UI.Cancel.performed += OnResumePerformed;
    }

    private void OnResumePerformed(InputAction.CallbackContext context)
    {
        ResumePerformed?.Invoke();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        PausePerformed?.Invoke();
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        FirePerformed?.Invoke();
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        JumpPerformed?.Invoke();
    }

    public Vector2 GetHorizontalMovement()
    {
        return _inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return _inputActions.Player.Look.ReadValue<Vector2>();
    }

    public void SwitchPlayerToUI()
    {
        _inputActions.Player.Disable();
        _inputActions.UI.Enable();
    }

    public void SwitchUIToPlayer()
    {
        _inputActions.UI.Disable();
        _inputActions.Player.Enable();
    }

}
