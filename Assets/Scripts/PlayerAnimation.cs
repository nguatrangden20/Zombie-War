using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    private Animator animator;
    private float animationBlendCount;

    public void FireAnimation()
    {
        animator.SetTrigger("Fire");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimationMovement();
    }

    private void AnimationMovement()
    {
        if (inputManager.IsFirePress)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        if (inputManager.detalMovement == Vector2.zero)
        {
            animationBlendCount -= Time.deltaTime * 20;
        }
        else
        {
            animationBlendCount += Time.deltaTime * 20;
        }
        
        animationBlendCount = Math.Clamp(animationBlendCount, 0f, 6f);
        animator.SetFloat("Speed", animationBlendCount);
    }
    private void OnFootstep(AnimationEvent animationEvent)
    {
    }
}
