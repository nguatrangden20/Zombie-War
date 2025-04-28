using UnityEngine;
using UnityEngine.AI;

public class DamagedState : IZombieState
{
    private ZombieAnimation zombieAnimation;
    private NavMeshAgent agent;
    private float timer;

    public DamagedState(ZombieAnimation zombieAnimation, NavMeshAgent agent)
    {
        this.zombieAnimation = zombieAnimation;
        this.agent = agent;
    }

    public void Enter(ZombieController zombie)
    {
        timer = 0;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        zombieAnimation.TriggerAnimation(AnimationName.Damaged);
    }

    public void Execute(ZombieController zombie)
    {
        timer += Time.deltaTime;
        if (timer < 0.2f) return;

        if (zombieAnimation.IsAnimationDone(AnimationName.Damaged))
        {
            zombie.ChangeState(zombie.ChaseState);
        }
    }

    public void Exit(ZombieController zombie)
    {
    }
}
