using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxTile : MonoBehaviour
{
    private Camera _camera;
    private float _previousCameraX;
    private float _startX;
    private float _width;

    [SerializeField] float _parallaxFactor;



    void Start()
    {
        _camera = Camera.main;

        _startX = transform.localPosition.x;

        _width = GetComponent<SpriteRenderer>().bounds.size.x;

        Debug.Log($"width {_width}");
    }


    void Update()
    {
        float cameraDeltaX = _camera.transform.position.x - _previousCameraX;

        Vector2 position = transform.position;
        position.x -= cameraDeltaX * _parallaxFactor;
        transform.position = position;

        _previousCameraX = _camera.transform.position.x;

        Repeat(cameraDeltaX: cameraDeltaX);
    }

    private void Repeat(float cameraDeltaX)
    {
        Vector2 position = transform.localPosition;
        if (cameraDeltaX > 0)
        {            
            if (transform.localPosition.x <= _startX - _width)
            {
                position.x = _startX;
            }            
        }
        else if (cameraDeltaX < 0) 
        {
            if (transform.localPosition.x >= _startX + _width)
            {
                position.x = _startX;
            }
        }
        transform.localPosition = position;
    }
}
