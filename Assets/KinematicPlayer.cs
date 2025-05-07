using UnityEngine;


public class KinematicPlayer: Player
{
    private float Gravity = -40f;

    private Vector2 _velocity = Vector2.zero;

    override protected void Start()
    {
        base.Start();
        if (Rigidbody.bodyType != RigidbodyType2D.Kinematic)
        {
            throw new UnityException("KinematicPlayer with non-Kinematic RigidBody");
        }
    }

    protected override void SetVelocityX(float velocityX)
    {
        _velocity.x = velocityX;
    }

    protected override void Jump()
    {
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdatePhysics();
    }

    private void UpdatePhysics()
    {                
        if (IsGrounded)
        {
            // Si estoy sobre el suelo no me muevo verticalmente
            _velocity.y = 0;
        }
        else
        {
            // Si estoy en el aire, aplico la aceleración de la gravedad a la velocidad vertical
            _velocity.y += Gravity * Time.deltaTime;
        }

        // Aplico la velocidad a la posición
        Vector3 position = transform.position;
        position.x += _velocity.x * Time.deltaTime;
        position.y += _velocity.y * Time.deltaTime;
        transform.position = position;
    }
}
