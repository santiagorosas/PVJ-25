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

    public void SetAnimation(Animation animation)
    {
        _animator.Play(stateName: animation.ToString());
    }
}
