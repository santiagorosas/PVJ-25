using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{    
    private List<int> horizontalInputStack = new();
    private int horizontalDir;

    private Wall LeftWall;
    private Wall RightWall;

    /*
    [SerializeField] private Wall LeftWall;
    [SerializeField] private Wall RightWall;
    */


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

    private static readonly Dictionary<KeyCode, int> horizontalInputs = new Dictionary<KeyCode, int> {
        {KeyCode.A, -1},
        {KeyCode.D, 1}
    };

    void Update()
    {
        UpdateInputStack(horizontalInputStack, horizontalInputs);
        horizontalDir = horizontalInputStack.LastOrDefault();

        float newX = transform.position.x + horizontalDir * Speed * Time.deltaTime;                
        newX = Mathf.Clamp(newX, LeftWallRightEdgeX + HalfWidth, RightWallLeftEdgeX - HalfWidth);

        Debug.Log("new X: " + newX);
        Debug.Log("LeftWallRightEdgeX: " + LeftWallRightEdgeX);
        Debug.Log("HalfWidth: " + HalfWidth);

        Vector3 position = transform.position;
        position.x = newX;
        transform.position = position;        
    }

    // Actualiza el orden de los inputs
    private void UpdateInputStack(List<int> stack, Dictionary<KeyCode, int> inputs)
    {    
        foreach (KeyValuePair<KeyCode, int> input in inputs)
            if (Input.GetKeyDown(input.Key) && !stack.Contains(input.Value))
                stack.Add(input.Value);
            else if (Input.GetKeyUp(input.Key))
                stack.Remove(input.Value);
    }
}
