using UnityEngine;

public class ZombiePool : GenericPool<ZombieController>
{
    [SerializeField] private BloodPool bloodPool;
    [SerializeField] private Transform target;
    protected override ZombieController CreatePooledItem()
    {
        var obj = Instantiate(prefab, transform);
        var poolable = obj.GetComponent<IPoolable>();
        if (poolable != null)
            poolable.SetPool(Pool);

        var zombie = obj as ZombieController;
        zombie.Target = target;
        zombie.BloodPool = bloodPool;

        return obj;
    }
}
