using UnityEngine;

public class DynamicPlayer : Player
{
    override protected void Start()
    {
        base.Start();
        if (Rigidbody.bodyType != RigidbodyType2D.Dynamic)
        {
            throw new UnityException("DynamicPlayer with non-dynamic RigidBody");
        }
    }

    override protected void SetVelocityX(float velocityX)
    {
        Vector3 velocity = Rigidbody.linearVelocity;
        velocity.x = velocityX;
        Rigidbody.linearVelocity = velocity;
    }

    override protected void Jump()
    {
        Vector3 velocity = Rigidbody.linearVelocity;
        velocity.y = JumpSpeed;
        Rigidbody.linearVelocity = velocity;
    }
}
