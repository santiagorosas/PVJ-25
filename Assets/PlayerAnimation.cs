using System;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum Animation
    {
        Idle,
        Walk,
        Jump,
        Attack
    }

    [SerializeField] private Animator _animator;

    public bool IsAttacking 
    { 
        get
        {
            return _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        } 
    }

    void Start()
    {
        
    }

    /*
    public void SetIdle()
    {
        _animator.SetTrigger("Stay");
        //SetAnimation(Animation.Idle);
    }

    public void SetWalk()
    {
        _animator.SetTrigger("Walk");
        //SetAnimation(Animation.Walk);
    }
    */

    public void SetAttack()
    {
        _animator.SetTrigger("Attack");
        Debug.Log("set attack");
    }

    private void SetAnimation(Animation animation)
    {
        _animator.Play(stateName: animation.ToString());
    }
    
    public void SetAbsoluteSpeedX(float absoluteSpeedX)
    {
        _animator.SetFloat("xSpeed", absoluteSpeedX);
    }

    internal void SetSpeedY(float speedY)
    {
        _animator.SetFloat("ySpeed", speedY);
    }

    public void SetJump()
    {
        SetAnimation(Animation.Jump);
    }

    internal void ExitJump()
    {
        _animator.SetTrigger("ExitJump");
    }
}
