using UnityEngine;

public class AttackState : IZombieState
{
    private ZombieSO data;
    private Transform target;
    private ZombieAnimation zombieAnimation;
    public AttackState(ZombieSO data, Transform target, ZombieAnimation zombieAnimation)
    {
        this.data = data;
        this.target = target;
        this.zombieAnimation = zombieAnimation;
    }
    public void Enter(ZombieController zombie)
    {
    }

    public void Execute(ZombieController zombie)
    {
        var distance = Vector3.Distance(zombie.transform.position, target.position);

        if (TransitionsChaseSate(distance, zombie) || !zombieAnimation.IsAnimationDone(TriggerName.Attack)) return;

        Vector3 direction = target.position - zombie.transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            zombie.transform.rotation = Quaternion.Slerp(zombie.transform.rotation, targetRotation, Time.deltaTime * data.RotationSpeed);

            float angle = Quaternion.Angle(zombie.transform.rotation, targetRotation);
            if (angle <= data.RotationThreshold)
            {
                zombieAnimation.TriggerAnimation(TriggerName.Attack);
            }
        }
    }

    private bool TransitionsChaseSate(float distance, ZombieController zombie)
    {
        if (distance > data.AttackDistance)
        {
            if (zombieAnimation.IsAnimationDone(TriggerName.Attack))
            {
                zombie.ChangeState(zombie.ChaseState);
            }
            return true;
        }
        return false;
    }

    public void Exit(ZombieController zombie)
    {
    }
}
