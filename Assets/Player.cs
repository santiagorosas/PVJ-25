using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput _input;
    private InputAction _moveAction;
    private Rigidbody2D _rigidbody;

    [SerializeField] private LayerMask _groundLayer;

    private float WalkSpeed { get => 10; }
    private float JumpSpeed { get => 20; }

    

    private bool IsGrounded 
    {
        get 
        {
            Debug.DrawRay(start: transform.position, dir: Vector2.down);
            RaycastHit2D raycastHit = Physics2D.Raycast(
                origin: transform.position, 
                direction: Vector2.down, 
                distance: 4,
                layerMask: _groundLayer
                ); 
            
            Debug.Log("collider: " + raycastHit.collider);
            
            return raycastHit.collider != null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Move"];

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = _moveAction.ReadValue<Vector2>().x;        
        float movementX = inputX * WalkSpeed;
        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.x = movementX;
        _rigidbody.linearVelocity = velocity;

        Debug.Log("Is grounded? " + IsGrounded);
    }    

    public void OnJumpInput() 
    {        
        if (IsGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.y = JumpSpeed;
        _rigidbody.linearVelocity = velocity;
    }

}
