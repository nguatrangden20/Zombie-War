using UnityEngine;
using UnityEngine.AI;

public class DamagedState : IZombieState
{
    private ZombieAnimation zombieAnimation;
    private NavMeshAgent agent;
    private BloodPool bloodPool;
    private float timer;

    public DamagedState(ZombieAnimation zombieAnimation, NavMeshAgent agent, BloodPool bloodPool)
    {
        this.zombieAnimation = zombieAnimation;
        this.agent = agent;
        this.bloodPool = bloodPool;
    }

    public void Enter(ZombieController zombie)
    {
        timer = 0;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        zombieAnimation.TriggerAnimation(TriggerName.Damaged);

        var effect = bloodPool.Pool.Get();
        effect.transform.parent = zombie.transform;
        effect.transform.localPosition = new Vector3(0, 1, 0);
    }

    public void Execute(ZombieController zombie)
    {
        timer += Time.deltaTime;
        if (timer < 0.2f) return;

        if (zombieAnimation.IsAnimationDone(TriggerName.Damaged))
        {
            zombie.ChangeState(zombie.ChaseState);
        }
    }

    public void Exit(ZombieController zombie)
    {
    }
}
