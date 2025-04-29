using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 DetalMovement {  get; set; }
    public Vector2 DetalLook { get; set; }

    public bool IsFirePress { get; set; }

    public Action OnC4Click;
    public Action OnSwitchClick;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.C4.performed += C4_performed;
        inputActions.Player.SwitchWeapon.performed += SwitchWeapon_performed;
        inputActions.Player.Move.performed += Move_performed;
        inputActions.Player.Move.canceled += Move_performed;
        inputActions.Player.Look.performed += Look_canceled;
        inputActions.Player.Look.canceled += Look_canceled;
        inputActions.Player.Attack.performed += Attack_performed;
        inputActions.Player.Attack.canceled += Attack_canceled;
    }

    private void Update()
    {
        if (PlayerHeath.IsGameOver) gameObject.SetActive(false);
    }

    private void Attack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       // IsFirePress = false;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       // IsFirePress = true;
    }

    private void Look_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //PosLook = obj.ReadValue<Vector2>();
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        DetalMovement = obj.ReadValue<Vector2>();
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
