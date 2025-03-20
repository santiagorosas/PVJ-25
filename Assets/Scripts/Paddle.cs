using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    private float Speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            position.x -= Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            position.x += Speed * Time.deltaTime;
        }

        transform.position = position;        
    }
}
