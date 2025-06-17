using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public int Speed { get => 3; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<PlayerAttackWave>())
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        ExplosionManager.Instance.CreateExplosion(position: transform.position);
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float playerX = Utils.Find<Player>().transform.position.x;
        int direction;
        if (playerX < transform.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        _rigidbody.linearVelocityX = direction * Speed;

        GetComponent<SpriteRenderer>().flipX = (direction == 1);
    }
}

