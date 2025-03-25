using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    private List<int> horizontalInputStack = new();
    private int horizontalDir;
    private float speed = 5;

    private static readonly Dictionary<KeyCode, int> horizontalInputs = new Dictionary<KeyCode, int> {
        {KeyCode.A, -1},
        {KeyCode.D, 1}
    };

    void Update()
    {
        UpdateInputStack(horizontalInputStack, horizontalInputs);
        horizontalDir = horizontalInputStack.LastOrDefault();
        transform.position += new Vector3(horizontalDir, 0, 0) * speed * Time.deltaTime;
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
