using System;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.linearVelocity = new Vector2(10,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 velocity = _rigidbody.linearVelocity;
        string colliderName = collider.name;
        
        if (colliderName == "TopWall") 
        {
            velocity.y = -velocity.y;
        }
        else if (colliderName == "LeftWall" || colliderName == "RightWall")
        {
            velocity.x = -velocity.x;
        }
        else if (colliderName == "Paddle") 
        {
            float xPaddle = collider.transform.position.x;
            float xBall = transform.position.x;
            float bounceSpeed = 15;
            velocity.x = (xBall - xPaddle) * bounceSpeed;
            velocity.y = -velocity.y;
        }
        else if (collider.GetComponent<Block>() != null) 
        {
            CollideWithBlock(collider.GetComponent<Block>());
        }        
        _rigidbody.linearVelocity = velocity;
    }

    private void CollideWithBlock(Block block)
    {
        Vector2 velocity = _rigidbody.linearVelocity;        
        _rigidbody.linearVelocity = velocity;
    }
}
