using UnityEngine;

public class Block : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Ball>() != null)
        {
            Destroy(gameObject);
        }
    }
}
