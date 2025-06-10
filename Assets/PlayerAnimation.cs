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

    void Start()
    {
        
    }

    public void SetIdle()
    {
        SetAnimation(Animation.Idle);
    }

    public void SetWalk()
    {
        SetAnimation(Animation.Walk);
    }

    public void SetJump()
    {
        SetAnimation(Animation.Jump);
    }

    public void SetAttack()
    {
        SetAnimation(Animation.Attack);
    }

    public void SetAnimation(Animation animation)
    {
        _animator.Play(stateName: animation.ToString());
    }
}
