using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using UnityEngine.Rendering.UI;

public class ZombieController : MonoBehaviour, IHP, IPoolable
{
    public IObjectPool<ZombieController> pool;
    public void SetPool<T>(IObjectPool<T> pool) where T : class
    {
        this.pool = pool as IObjectPool<ZombieController>;
    }

    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0) ChangeState(DeathState);
            else ChangeState(DamagedState);
        }
    }

    public AttackState AttackState;
    public ChaseState ChaseState;
    public DeathState DeathState;
    public DamagedState DamagedState;
    public Transform Target;
    public BloodPool BloodPool;

    [SerializeField] private ZombieSO data;
    [SerializeField] private ZombieAnimation zombieAnimation;
    [SerializeField] private Collider zombieCollider;
    [SerializeField] private Renderer zombieRender;

    private NavMeshAgent agent;
    private IZombieState currentState;
    private float hp;

    public void ReturnToPool()
    {
        pool.Release(this);
    }

    public void ChangeState(IZombieState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetUp(int difficulty)
    {
        hp = data.HP + difficulty * 10;
    }

    private void Awake()
    {
        zombieCollider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = data.AttackDistance;
    }

    private void Start()
    {
        AttackState = new AttackState(data, Target, zombieAnimation);
        ChaseState = new ChaseState(Target, data, agent);
        DamagedState = new DamagedState(zombieAnimation, agent, BloodPool);
        DeathState = new DeathState(zombieAnimation, agent, zombieCollider, zombieRender.material);

        ChangeState(ChaseState);
    }

    private void OnEnable()
    {
        hp = data.HP;
        zombieCollider.enabled = true;

        if (currentState != null)
            ChangeState(ChaseState);
    }

    void Update()
    {
        if (PlayerHeath.IsGameOver)
        {
            HP = 0;
        }

        currentState.Execute(this);

        if (Vector3.Distance(transform.position, Target.position) > 15f)
        {
            ReturnToPool();
        }
    }
}
