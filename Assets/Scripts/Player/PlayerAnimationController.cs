using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public float AnimationSmoothTime = 5f;

    private Vector2 CurrentMovementAnimationBlend = new Vector2();
    private Vector2 CurrentBlendVelocity;

    private Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    public void SetAnimationMove(Vector2 InputVector)
    {
        CurrentMovementAnimationBlend = Vector2.SmoothDamp(CurrentMovementAnimationBlend, InputVector, ref CurrentBlendVelocity, AnimationSmoothTime * Time.deltaTime);
        _Animator.SetFloat("X", CurrentMovementAnimationBlend.x);
        _Animator.SetFloat("Z", CurrentMovementAnimationBlend.y);
    }
}
