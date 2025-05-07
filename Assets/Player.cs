using System;
using UnityEngine;
using UnityEngine.InputSystem;

abstract public class Player : MonoBehaviour
{
    private PlayerInput _input;
    private InputAction _moveAction;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    
    [SerializeField] private LayerMask _groundLayerMask;

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

    private bool RaycastGround()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(
                origin: (Vector2)transform.position + GetComponent<Collider2D>().offset,
                direction: Vector2.down,
                distance: 1,
                layerMask: _groundLayerMask
                );

        return raycastHit.collider != null;
    }
    
    virtual protected void Start()
    {
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Move"];

        _rigidbody = GetComponent<Rigidbody2D>();
    }


    virtual protected void FixedUpdate()
    {
        float inputX = _moveAction.ReadValue<Vector2>().x;
        float movementX = inputX * WalkSpeed;
        SetVelocityX(movementX);
    }
        
    public void OnJumpInput() 
    {        
        if (IsGrounded)
        {            
            Jump();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(origin: transform.position, direction: Vector3.down));
    }

    bool IsLayerInMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}
