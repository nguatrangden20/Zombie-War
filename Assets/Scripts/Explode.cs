using UnityEngine;

public class Explode : MonoBehaviour
{
    private Vector3 velocity;
    private bool isExploded = false;
    private float deceleration = 3f;

    public void Explosion(Vector3 explosionPosition, float explosionRadius, float maxForce)
    {
        Vector3 direction = (transform.position - explosionPosition).normalized;
        direction.y = 0;
        float distance = Vector3.Distance(transform.position, explosionPosition);

        float force = Mathf.Clamp(maxForce * (1 - distance / explosionRadius), 0, maxForce);

        velocity = direction * force;
        isExploded = true;


        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(-direction);
            transform.rotation = lookRotation;
        }
    }

    private void Update()
    {
        if (!isExploded) return;

        Debug.Log(velocity);
        transform.position += velocity * Time.deltaTime;

        velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);

        if (velocity.magnitude < 0.1f)
        {
            isExploded = false;
            velocity = Vector3.zero;
        }
    }
}
