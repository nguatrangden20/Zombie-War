using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speedMove, speedRotation;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        var detalMove = inputManager.detalMovement * speedMove * Time.deltaTime;
        characterController.Move(new Vector3(detalMove.x, 0, detalMove.y));
    }

    private void Rotation()
    {
        if (inputManager.detalMovement == Vector2.zero) return;

        Vector3 direction = new Vector3(inputManager.detalMovement.x, 0f, inputManager.detalMovement.y);

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
    }
}
