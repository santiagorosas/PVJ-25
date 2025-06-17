using UnityEngine;

public class Enemy : MonoBehaviour
{
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
}

