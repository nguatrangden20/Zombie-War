using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPoolable
{
    public float Damage = 10;
    public IObjectPool<Bullet> pool;
    public Vector3 Direction;

    [SerializeField] private float speed = 10;
    private TrailRenderer trail;

    public void SetPool<T>(IObjectPool<T> pool) where T : class
    {
        this.pool = pool as IObjectPool<Bullet>;
    }

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    public void FireBullet(Vector3 pos)
    {
        trail.enabled = false;
        transform.position = pos;
        trail.enabled = true;

        StartBullet();
    }

    private void StartBullet()
    {
        Vector3 HitPoint;
        if (Physics.Raycast(transform.position, Direction, out RaycastHit hit, float.MaxValue))
        {
            HitPoint = hit.point;
        }
        else
        {
            HitPoint = transform.position + Direction * 100;
        }

        IHP DamageAble = null;
        if (hit.collider != null)
        {
            DamageAble = hit.collider.gameObject.GetComponent<IHP>();
        }
        StartCoroutine(SpawnTrail(trail, HitPoint, DamageAble));
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, IHP DamageAble)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= speed * Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(Trail.time);

        if (DamageAble != null)
        {
            DamageAble.HP -= Damage;
        }

        pool.Release(this);
    }
}
