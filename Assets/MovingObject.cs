using System;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    void Start()
    {
        transform.position = new Vector2(x: 3, y: 0);
    }

    void Update()
    {
        Vector2 currentPosition = transform.position;
        currentPosition.y -= Time.deltaTime * 3f;
        transform.position = currentPosition;
    }
}
