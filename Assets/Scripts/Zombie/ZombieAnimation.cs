using System;
using UnityEngine;
using UnityEngine.AI;

public enum AnimationName
{
    Damaged,
    Attack,
    Death,
    Chasing
}
public class ZombieAnimation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speedBlendMove = 3;

    private Animator animator;
    private float animationBlendCount;

    public void TriggerAnimation(AnimationName name)
    {
        animator.SetTrigger(name.ToString());
    }

    public bool IsAnimationDone(AnimationName name)
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
        animator.SetFloat(AnimationName.Chasing.ToString(), animationBlendCount);
    }
}
