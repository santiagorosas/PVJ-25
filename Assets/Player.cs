using System;
using UnityEngine;
using UnityEngine.InputSystem;

abstract public class Player : MonoBehaviour
{
    private PlayerInput _input;
    private InputAction _moveAction;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private PlayerAnimation _animation;
    
    //[SerializeField] private LayerMask _groundLayerMask;

    private float WalkSpeed { get => 10; }
    protected float JumpSpeed { get => 20; }

    protected Rigidbody2D Rigidbody { get => _rigidbody; }

    abstract protected void SetVelocityX(float movementX);
    abstract protected void Jump();

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {        
        _isGrounded = true;        
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {        
        _isGrounded = false;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }

    protected bool IsGrounded 
    {
        get 
        {
            return _isGrounded;            
        }
    }

    public bool IsAttacking 
    { 
        get
        {
            return _animation.IsAttacking;
        } 
    }

    virtual protected void Start()
    {
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Move"];

        _rigidbody = GetComponent<Rigidbody2D>();

        _animation = GetComponent<PlayerAnimation>();
    }

    virtual protected void FixedUpdate()
    {
        if (!IsAttacking)
        {
            UpdateVelocityX();   
        }
        
        UpdateAnimation();
        UpdateFlipX();
    }

    private void UpdateVelocityX()
    {
        float inputX = _moveAction.ReadValue<Vector2>().x;
        float movementX = inputX * WalkSpeed;
        SetVelocityX(movementX);
    }

    private void UpdateFlipX()
    {
        Vector3 scale = transform.localScale;
        if (_rigidbody.linearVelocityX < 0)
        {
            scale.x = -1;
        }
        else if (_rigidbody.linearVelocityX > 0)
        {
            scale.x = 1;
        }
        transform.localScale = scale;
    }

    private void UpdateAnimation()
    {
        _animation.SetAbsoluteSpeedX(Mathf.Abs(_rigidbody.linearVelocityX));

        if (_rigidbody.linearVelocityY != 0)
        {
            _animation.SetJump();
        }
        else
        {
            _animation.ExitJump();
        }      
    }

    public void OnJumpInput() 
    {        
        if (IsGrounded)
        {            
            Jump();
        }
    }

    public void OnAttackInput()
    {
        Debug.Log("attack");
        if (IsGrounded && !IsAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animation.SetAttack();
    }

    bool IsLayerInMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}
