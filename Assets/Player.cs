using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float SPEED = 10;

    [SerializeField] private float _speed;

    private float Speed 
    {
        get => SPEED;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else
        {
            Stop();
        }
    }

    private void Stop()
    {
        GetComponent<Rigidbody2D>().linearVelocityX = 0;
    }

    private void MoveLeft()
    {
        GetComponent<Rigidbody2D>().linearVelocityX = -Speed;
        
        /*
        Vector2 currentPosition = transform.position;
        currentPosition.x -= Time.deltaTime * Speed;
        transform.position = currentPosition;
        */
    }

    private void MoveRight()
    {
        GetComponent<Rigidbody2D>().linearVelocityX = Speed;
        /*
        Vector2 currentPosition = transform.position;
        currentPosition.x += Time.deltaTime * Speed;
        transform.position = currentPosition;
        */
    }
}
