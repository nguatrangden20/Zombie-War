using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour, IHP
{
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
    public Transform taget;

    [SerializeField] private ZombieSO data;
    [SerializeField] private ZombieAnimation zombieAnimation;
    [SerializeField] private Collider zombieCollider;
    [SerializeField] private BloodPool bloodPool;
    [SerializeField] private Renderer zombieRender;

    private NavMeshAgent agent;
    private IZombieState currentState;
    private float hp;

    public void ChangeState(IZombieState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    private void Awake()
    {
        zombieCollider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = data.AttackDistance;

        AttackState = new AttackState(data, taget, zombieAnimation);
        ChaseState = new ChaseState(taget, data, agent);
        DamagedState = new DamagedState(zombieAnimation, agent, bloodPool);
        DeathState = new DeathState(zombieAnimation, agent, zombieCollider, zombieRender.material);
    }

    private void OnEnable()
    {
        hp = data.HP;
        zombieCollider.enabled = true;

        currentState = ChaseState;
        currentState.Enter(this);
    }

    void Update()
    {
        currentState.Execute(this);
    }
}
