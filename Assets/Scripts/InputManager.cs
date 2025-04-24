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
