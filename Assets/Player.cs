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
    private PlayerAttackWave _attackWave;
    private float _timeSinceLastAttack = ATTACK_COOLDOWN;

    //[SerializeField] private LayerMask _groundLayerMask;

    private float WalkSpeed { get => 10; }
    protected float JumpSpeed { get => 20; }

    protected const float ATTACK_COOLDOWN = 0.5f;
    
    private bool CanAttack { get => _timeSinceLastAttack >= ATTACK_COOLDOWN; }

    protected Rigidbody2D Rigidbody { get => _rigidbody; }

    abstract protected void SetVelocityX(float movementX);
    abstract protected void Jump();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsCollisionGround(collision))
        {
            Debug.Log("collision enter " + collision.gameObject.name);
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsCollisionGround(collision))
        {
            Debug.Log("collision exit " + collision.gameObject.name);
            _isGrounded = false;
        }
    }

    private bool IsCollisionGround(Collision2D collision)
    {
        return collision.gameObject.tag == "Ground";
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

        _attackWave = GetComponentInChildren<PlayerAttackWave>();
        _attackWave.gameObject.SetActive(false);

        if (_attackWave == null)
        {
            throw new UnityException("no attack wave");
        }
    }

    virtual protected void FixedUpdate()
    {
        UpdateVelocityX();   
        UpdateAnimation();
        UpdateFlipX();
        UpdateAttack();
    }

    private void UpdateAttack()
    {
        if (_input.actions["Attack"].IsPressed() && CanAttack)
        {
            Attack();
        }
        else
        {
            //_animation.EndAttack();
        }

        _timeSinceLastAttack += Time.deltaTime;
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
        
        if (_rigidbody.linearVelocityY == 0 && _rigidbody.linearVelocityX != 0)
        {
            _animation.SetIsWalking(true);
        }
        else
        {
            _animation.SetIsWalking(false);
        }
    }

    public void OnJumpInput() 
    {
        if (IsGrounded)
        {            
            Jump();
        }
    }

    private void Attack()
    {
        _animation.SetAttack();
        _attackWave.Attack();
        _timeSinceLastAttack = 0;
    }

    bool IsLayerInMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}
