using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IZombieState
{
    private Transform target;
    private ZombieSO data;
    private NavMeshAgent agent;
    private float destinationUpdateTimer;

    public ChaseState(Transform target, ZombieSO data, NavMeshAgent agent) 
    { 
        this.target = target;
        this.data = data;
        this.agent = agent;
    }

    public void Enter(ZombieController zombie)
    {
        agent.isStopped = false;
        destinationUpdateTimer = data.DestinationRefreshRate;
    }

    public void Execute(ZombieController zombie)
    {
        var distance = Vector3.Distance(zombie.transform.position, target.position);
        if (distance < data.AttackDistance) zombie.ChangeState(zombie.AttackState);


        if (destinationUpdateTimer < data.DestinationRefreshRate)
        {
            destinationUpdateTimer += Time.deltaTime;
        }
        else
        {
            agent.destination = target.position;
            destinationUpdateTimer = 0;
        }
    }

    public void Exit(ZombieController zombie)
    {
    }
}
