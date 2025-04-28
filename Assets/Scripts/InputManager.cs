using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 detalMovement
    {
        get
        {
            return inputActions.Player.Move.ReadValue<Vector2>();
        }
    }
    public Vector2 PosLook
    {
        get
        {
            return inputActions.Player.Look.ReadValue<Vector2>();
        }
    }

    public bool IsFirePress
    {
        get
        {
            return inputActions.Player.Attack.IsPressed();
        }
    }

    public Action OnC4Click;
    public Action OnSwitchClick;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.C4.performed += C4_performed;
        inputActions.Player.SwitchWeapon.performed += SwitchWeapon_performed;
    }

    private void SwitchWeapon_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSwitchClick?.Invoke();
    }

    private void C4_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnC4Click?.Invoke();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
