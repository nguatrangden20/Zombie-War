using UnityEngine;

public class PlayerShotting : MonoBehaviour
{
    public float FireRate;

    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);

    private float fireCount;
    public Vector3 direction;

    private void Update()
    {
        fireCount += Time.deltaTime;

        if (inputManager.IsFirePress && fireCount >= FireRate)
        {
            var bullet = bulletPool.Pool.Get();
            bullet.SetStartPosition(bulletSpawnPoint.position);

            direction = GetDirection();

            if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue))
            {
                bullet.StartBullet(hit.point, hit);
                playerAnimation.FireAnimation();
            }
            else
            {
                bullet.StartBullet(bulletSpawnPoint.position + direction * 100, new RaycastHit());
                playerAnimation.FireAnimation();
            }

            fireCount = 0;
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;


        direction += new Vector3(
            Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
            Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
            Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
        );

        direction.Normalize();


        return direction;
    }
}
