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

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
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
