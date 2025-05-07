using System;
using UnityEngine;
using UnityEngine.InputSystem;

abstract public class Player : MonoBehaviour
{
    private PlayerInput _input;
    private InputAction _moveAction;
    private Rigidbody2D _rigidbody;
    
    
    [SerializeField] private LayerMask _groundLayer;

    private float WalkSpeed { get => 10; }
    protected float JumpSpeed { get => 20; }

    protected Rigidbody2D Rigidbody { get => _rigidbody; }

    abstract protected void SetVelocityX(float movementX);
    abstract protected void Jump();

    private bool IsGrounded 
    {
        get 
        {            
            RaycastHit2D raycastHit = Physics2D.Raycast(
                origin: transform.position, 
                direction: Vector2.down, 
                distance: 4,
                layerMask: _groundLayer
                );
            
            return raycastHit.collider != null;
        }
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
}
