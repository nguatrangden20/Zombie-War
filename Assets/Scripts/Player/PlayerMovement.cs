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
        DrawLineShoot();
    }

    private void Movement()
    {
        if (inputManager.IsFirePress) return;

        var detalMove = inputManager.detalMovement * speedMove * Time.deltaTime;
        characterController.Move(new Vector3(detalMove.x, 0, detalMove.y));
    }
    private void Rotation()
    {
        if (inputManager.IsFirePress)
            RotationBaseFire();
        else
            RotationBaseMovement();
    }
    private void RotationBaseFire()
    {
        if (inputManager.PosLook == Vector2.zero) return;
        Ray ray = Camera.main.ScreenPointToRay(inputManager.PosLook);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
        {
            Vector3 targetPoint = hitInfo.point;
            Vector3 directionMouse = targetPoint - transform.position;
            directionMouse.y = 0f;

            if (directionMouse.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotationMouse = Quaternion.LookRotation(directionMouse);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationMouse, speedRotation * Time.deltaTime);
            }
        }
    }
    private void RotationBaseMovement()
    {
        if (inputManager.detalMovement == Vector2.zero) return;

        Vector3 direction = new Vector3(inputManager.detalMovement.x, 0f, inputManager.detalMovement.y);

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
    }

    public Vector3 LastPointer = Vector3.zero;
    private void DrawLineShoot()
    {
        Debug.DrawLine(transform.position, LastPointer, Color.red);

        if (!inputManager.IsFirePress) return;

        Ray ray = Camera.main.ScreenPointToRay(inputManager.PosLook);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
        {
            LastPointer = hitInfo.point;
        }
    }
}
