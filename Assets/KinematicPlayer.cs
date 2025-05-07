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

        Vector3 position = transform.position;
        position.x += _velocity.x * Time.deltaTime;
        position.y += _velocity.y * Time.deltaTime;
        transform.position = position;
    }
}
