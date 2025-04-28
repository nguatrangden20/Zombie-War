using UnityEngine;
using UnityEngine.Pool;
public interface IPoolable
{
    void SetPool<T>(IObjectPool<T> pool) where T : class;
}
public class GenericPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool collectionChecks = true;
    public int maxPoolSize = 1000;

    [SerializeField] private T prefab;

    private IObjectPool<T> pool;

    public IObjectPool<T> Pool
    {
        get
        {
            if (pool == null)
                pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
            return pool;
        }
    }

    private T CreatePooledItem()
    {
        var obj = Instantiate(prefab, transform);
        var poolable = obj.GetComponent<IPoolable>();
        if (poolable != null)
            poolable.SetPool(Pool);

        return obj;
    }

    private void OnReturnedToPool(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(T obj)
    {
        Destroy(obj.gameObject);
    }
}
