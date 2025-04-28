using UnityEngine;

public abstract class Gun : MonoBehaviour, IGun
{
    public BulletPool bulletPool;
    public InputManager inputManager;
    public float FireRate = 0.2f;
    public Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);

    [SerializeField] protected Transform barrel;
    [SerializeField] protected GameObject effectFire;

    private float lastTimeFire = 0;

    public Bullet[] GetBullet()
    {
        if (lastTimeFire + FireRate <= Time.time)
        {
            lastTimeFire = Time.time;
            return HandleBullet();
        }
        else
            return null;
    }

    public abstract Bullet[] HandleBullet();

    protected virtual void Update()
    {
        if (inputManager.IsFirePress)
        {
            effectFire.SetActive(true);
        }
        else
        {
            effectFire.SetActive(false);
        }
    }

    protected Vector3 GetDirection()
    {
        Vector3 direction = barrel.forward;

        direction += new Vector3(
            Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
            Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
            Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
        );

        direction.Normalize();


        return direction;
    }
}
