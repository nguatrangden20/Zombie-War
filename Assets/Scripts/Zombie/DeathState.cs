using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DeathState : IZombieState
{
    private ZombieAnimation zombieAnimation;
    private NavMeshAgent agent;
    private Collider zombieCollider;
    private Material material;
    private float counter;

    public DeathState(ZombieAnimation zombieAnimation, NavMeshAgent agent, Collider zombieCollider, Material material)
    {
        this.zombieAnimation = zombieAnimation;
        this.agent = agent;
        this.zombieCollider = zombieCollider;
        this.material = material;
    }

    public void Enter(ZombieController zombie)
    {
        if (zombieCollider.enabled == false) return;

        counter = 0;
        zombieCollider.enabled = false;
        agent.isStopped = true;
        zombieAnimation.TriggerAnimation(AnimationName.Death);
    }

    public void Execute(ZombieController zombie)
    {
        counter += Time.deltaTime * 0.5f;
        material.SetFloat("_Dissolve", Mathf.Clamp01(counter));
    }

    public void Exit(ZombieController zombie)
    {
        material.SetFloat("_Dissolve", 0);
    }
}
