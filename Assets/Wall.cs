using UnityEngine;

public class Wall : MonoBehaviour
{
    public float RightEdgeX { 
        get
        {
            return GetComponent<BoxCollider2D>().bounds.max.x;
        }
    }

    public float LeftEdgeX { 
        get
        {
            return GetComponent<BoxCollider2D>().bounds.min.x;
        }
    }
}
