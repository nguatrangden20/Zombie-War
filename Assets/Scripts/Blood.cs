using UnityEngine;
using UnityEngine.Pool;

public class Blood : MonoBehaviour, IPoolable
{
    private ParticleSystem ps;
    public IObjectPool<Blood> pool;

    public void SetPool<T>(IObjectPool<T> pool) where T : class
    {
        this.pool = pool as IObjectPool<Blood>;
    }

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps && !ps.IsAlive())
        {
            pool.Release(this);
        }
    }
}
