using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private float Speed 
    {
        get => Constants.Instance.BallSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.linearVelocity = new Vector2(10,10).normalized * Speed;
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
            float bounceSpeed = Speed;
            velocity.x = (xBall - xPaddle) * bounceSpeed;
            velocity.y = -velocity.y;
            SoundManager.PlaySound(SoundManager.instance.paddleHit);
        }
        else if (collider.GetComponent<Block>() != null) 
        {
            velocity = CollideWithBlock(collider);
        }        
        else if (colliderName == "FailBox") 
        {
            Lose();
        }

        _rigidbody.linearVelocity = velocity;
    }

    private void Lose()
    {
        SceneManager.LoadScene("Main");
    }


    private Vector3 CollideWithBlock(Collider2D block)
    {
        Vector3 velocity = _rigidbody.linearVelocity;

        float xBall = transform.position.x;
        float xBlock = block.transform.position.x;
        float bounceSpeed = Speed;
        velocity.x = (xBall - xBlock) * bounceSpeed;
        velocity.y = -velocity.y;
        _rigidbody.linearVelocity = velocity;

        SoundManager.PlaySound(SoundManager.instance.blockHit);

        return velocity;
    }

}
