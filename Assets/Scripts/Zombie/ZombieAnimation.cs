using System;
using UnityEngine;
using UnityEngine.AI;

public enum TriggerName
{
    Damaged,
    Attack,
    Death
}
public class ZombieAnimation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speedBlendMove = 3;

    private Animator animator;
    private float animationBlendCount;
    private string[] triggerNames;

    public void PlayOnlyOneTrigger(TriggerName name)
    {
        foreach (var triggerName in triggerNames)
        {
            animator.ResetTrigger(triggerName);
        }

        animator.SetTrigger(name.ToString());
    }

    public void TriggerAnimation(TriggerName name)
    {
        animator.SetTrigger(name.ToString());
    }

    public bool IsAnimationDone(TriggerName name)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(name.ToString()))
        {
            return false;
        }

        animator.ResetTrigger(name.ToString());

        return true;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        triggerNames = Enum.GetNames(typeof(TriggerName));
    }

    private void Update()
    {
        if (agent.velocity.magnitude != 0)
        {
            animationBlendCount += Time.deltaTime * speedBlendMove;
        }
        else
        {
            animationBlendCount -= Time.deltaTime * speedBlendMove;
        }

        animationBlendCount = Math.Clamp(animationBlendCount, 0f, 1f);
        animator.SetFloat("Chasing", animationBlendCount);
    }
}
