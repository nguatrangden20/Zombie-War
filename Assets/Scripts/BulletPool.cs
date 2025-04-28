using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public bool collectionChecks = true;
    public int maxPoolSize = 1000;

    [SerializeField] private Bullet prefab;

    IObjectPool<Bullet> m_Pool;

    public IObjectPool<Bullet> Pool
    {
        get
        {
            if (m_Pool == null)
                m_Pool = new ObjectPool<Bullet>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
            return m_Pool;
        }
    }

    Bullet CreatePooledItem()
    {
        var bullet = Instantiate(prefab, transform);
        bullet.pool = Pool;
        return bullet;
    }

    void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
