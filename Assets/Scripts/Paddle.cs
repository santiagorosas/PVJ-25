using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


public class Paddle : MonoBehaviour
{
    private static readonly Dictionary<string, int> _directionsByInputActionName = new Dictionary<string, int> {
        {"MoveLeft", -1},
        {"MoveRight", 1}
    };
    private List<int> _horizontalInputs = new();
    private int _horizontalDirection;

    private Wall LeftWall;
    private Wall RightWall;

    
    private float Speed 
    {
        get 
        {                        
            return Constants.Instance.PaddleSpeed;
        }        
    }

    public float LeftWallRightEdgeX 
    {         
        get => LeftWall.RightEdgeX;        
    }

    public float RightWallLeftEdgeX 
    {         
        get => RightWall.LeftEdgeX;        
    }
    public float HalfWidth { get => GetComponent<BoxCollider2D>().size.x * transform.localScale.x / 2; }

    private void Start()
    {
        LeftWall = GameObject.Find("LeftWall").GetComponent<Wall>();
        RightWall = GameObject.Find("RightWall").GetComponent<Wall>();
    }    

    void Update()
    {
        UpdateInputStack();
        _horizontalDirection = _horizontalInputs.LastOrDefault();

        float newX = transform.position.x + _horizontalDirection * Speed * Time.deltaTime;                
        newX = Mathf.Clamp(newX, LeftWallRightEdgeX + HalfWidth, RightWallLeftEdgeX - HalfWidth);

        Vector3 position = transform.position;
        position.x = newX;
        transform.position = position;        
    }

    // Actualiza el orden de los inputs
    private void UpdateInputStack()
    {
        foreach (KeyValuePair<string, int> kv in _directionsByInputActionName)
        {
            string inputActionName = kv.Key;
            int direction = kv.Value;

            InputAction inputAction = InputSystem.actions.FindAction(inputActionName);

            if (inputAction.WasPressedThisFrame() && !_horizontalInputs.Contains(direction))
            {
                _horizontalInputs.Add(direction);
            }   
            else if (inputAction.WasReleasedThisFrame())
            {
                _horizontalInputs.Remove(direction);
            }       
        }
    }
}
