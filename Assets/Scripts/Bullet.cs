using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float Damage = 10;
    public IObjectPool<Bullet> pool;

    [SerializeField] private float speed = 10;
    private TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    public void SetStartPosition(Vector3 pos)
    {
        trail.enabled = false;
        transform.position = pos;
        trail.enabled = true;
    }

    public void StartBullet(Vector3 HitPoint, RaycastHit hit)
    {
        ZombieStateMachine zombie = null;
        if (hit.collider != null)
        {
            zombie = hit.collider.gameObject.GetComponent<ZombieStateMachine>();
        }
        StartCoroutine(SpawnTrail(trail, HitPoint, zombie));
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, ZombieStateMachine zombie)
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

        if (zombie != null)
        {
            zombie.HP -= Damage;
        }

        pool.Release(this);
    }
}
