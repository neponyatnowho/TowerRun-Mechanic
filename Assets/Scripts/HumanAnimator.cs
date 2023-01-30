using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Human))]
public class HumanAnimator : MonoBehaviour
{

    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void DiedAnimation()
    {
        StopRunAnimation();
        StopIdleAnimation();
        StopJumpAnimation();
        _animator.SetBool("isDead", true);
    }

    public void RunAnimation()
    {
        StopJumpAnimation();
        StopIdleAnimation();
        StopDiedAnimation();
        _animator.SetBool("isRunning", true);
    }

    public void IdleAnimation()
    {
        StopRunAnimation();
        StopJumpAnimation();
        StopDiedAnimation();
        _animator.SetBool("isIdle", true);
    }

    public void JumpAnimation()
    {
        StopRunAnimation();
        StopIdleAnimation();
        StopDiedAnimation();
        _animator.SetTrigger("isJump");
    }    
    public void StopJumpAnimation()
    {
        _animator.ResetTrigger("isJump");
    }


    public void StopDiedAnimation()
    {
        _animator.SetBool("isDead", false);
    }

    public void StopRunAnimation()
    {
        _animator.SetBool("isRunning", false);
    }

    public void StopIdleAnimation()
    {
        _animator.SetBool("isIdle", false);
    }



}
