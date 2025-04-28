using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    public BulletPool bulletPool;

    protected Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);

    public Bullet[] GetBullet()
    {
        throw new System.NotImplementedException();
    }

    protected Vector3 GetDirection()
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
