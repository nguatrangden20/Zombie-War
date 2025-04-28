using UnityEngine;
using UnityEngine.AI;
public class ZombieStateMachine : MonoBehaviour
{    
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0) state = ZombieState.Death;
            else state = ZombieState.Damaged;
        }
    }
    public float TimeUpdateDestination = 1;
    public Transform Target;

    [SerializeField] private float attackDistance;
    [SerializeField] private float rotationThreshold = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private ZombieAnimation zombieAnimation;

    private NavMeshAgent agent;
    private float TimeUpdateDestinationCount;
    private float distance;
    private float hp = 100;

    private enum ZombieState
    {
        Chasing,
        Attacking,
        Death,
        Damaged
    }
    private ZombieState state;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        state = ZombieState.Chasing;
        TimeUpdateDestinationCount = TimeUpdateDestination;
    }

    private void Update()
    {
        distance = Vector3.Distance(agent.transform.position, Target.position);

        switch (state)
        {
            case ZombieState.Attacking:
                HandleAttacking();
                break;
            case ZombieState.Death:
                HandleDeath();
                break;
            case ZombieState.Chasing:
                HandleChasing();
                break;
            case ZombieState.Damaged:
                HandleDamaged();
                break;
        }
    }

    private void HandleDamaged()
    {
        if (zombieAnimation.IsAnimationDone(AnimationName.Damaged) && agent.isStopped)
        {
            state = ZombieState.Chasing;
        }

        zombieAnimation.TriggerAnimation(AnimationName.Damaged);

        agent.isStopped = true;
    }

    private void HandleAttacking()
    {
        if (TransitionsChaseSate() || !zombieAnimation.IsAnimationDone(AnimationName.Attack)) return;

        Vector3 direction = Target.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            if (angle <= rotationThreshold)
            {
                zombieAnimation.TriggerAnimation(AnimationName.Attack);
            }
        }
    }

    private bool TransitionsChaseSate()
    {
        if (distance > attackDistance)
        {
            if (zombieAnimation.IsAnimationDone(AnimationName.Attack))
            {
                TimeUpdateDestinationCount = TimeUpdateDestination;
                state = ZombieState.Chasing;
            }
            return true;
        }
        return false;
    }

    private void HandleDeath()
    {

    }

    private void HandleChasing() 
    {
        TransitionAttackState();

        agent.isStopped = false;

        if (TimeUpdateDestinationCount < TimeUpdateDestination)
        {
            TimeUpdateDestinationCount += Time.deltaTime;
        }
        else
        {
            agent.destination = Target.position;
            TimeUpdateDestinationCount = 0;
        }
    }
    private void TransitionAttackState()
    {
        if (distance < attackDistance) state = ZombieState.Attacking;
    }
}
