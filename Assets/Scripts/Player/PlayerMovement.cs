using UnityEngine;

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
        if (inputManager.IsFirePress) return;

        var detalMove = inputManager.DetalMovement * speedMove * Time.deltaTime;
        characterController.Move(new Vector3(detalMove.x, 0, detalMove.y));
    }
    private void Rotation()
    {
        if (inputManager.IsFirePress)
            Rotation(inputManager.DetalLook);
        else
            Rotation(inputManager.DetalMovement);
    }
    private void Rotation(Vector2 detal)
    {
        if (detal == Vector2.zero) return;

        Vector3 direction = new Vector3(detal.x, 0f, detal.y);

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
    }
}
