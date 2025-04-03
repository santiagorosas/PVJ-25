using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    internal void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Ball>() != null)
        {
            Destroy(gameObject);
        }
    }
}
