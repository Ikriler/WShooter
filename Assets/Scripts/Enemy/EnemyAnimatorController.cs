using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackAnimation()
    {
        animator.SetFloat("Blend", 0.5f);
    }

    public void IdleAnimation()
    {
        animator.SetFloat("Blend", 1f);
    }

    public void MoveAnimation()
    {
        animator.SetFloat("Blend", 0f);
    }
}
