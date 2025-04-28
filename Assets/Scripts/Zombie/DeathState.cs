using UnityEngine;
using UnityEngine.AI;

public class DeathState : IZombieState
{
    private ZombieAnimation zombieAnimation;
    private NavMeshAgent agent;
    private Collider zombieCollider;

    public DeathState(ZombieAnimation zombieAnimation, NavMeshAgent agent, Collider zombieCollider)
    {
        this.zombieAnimation = zombieAnimation;
        this.agent = agent;
        this.zombieCollider = zombieCollider;
    }

    public void Enter(ZombieController zombie)
    {
        if (zombieCollider.enabled == false) return;

        zombieCollider.enabled = false;
        agent.isStopped = true;
        zombieAnimation.TriggerAnimation(AnimationName.Death);
    }

    public void Execute(ZombieController zombie)
    {
    }

    public void Exit(ZombieController zombie)
    {
    }
}
