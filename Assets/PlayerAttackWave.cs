using System;
using UnityEngine;

public class PlayerAttackWave : MonoBehaviour
{
    public float AttackDuration { get => 0.2f; }

    void Start()
    {
        //gameObject.SetActive(false);
    }

    public void Attack()
    {
        gameObject.SetActive(true);
        Invoke(nameof(AfterAttack), AttackDuration);
    }

    private void AfterAttack()
    {
        gameObject.SetActive(false);
    }
}
