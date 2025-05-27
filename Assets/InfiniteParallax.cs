using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InfiniteParallax : MonoBehaviour
{
    public float _parallaxFactor = 0.5f;

    private Transform _cameraTransform;
    private float _width; // Width of one tile in world units
    private float _startX;
    private float _lastCameraX;

    void Start()
    {
        _startX = transform.position.x - Camera.main.transform.position.x;
        _width = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log("width: " + _width);

        if (_cameraTransform == null)
            _cameraTransform = Camera.main.transform;

        _lastCameraX = _cameraTransform.position.x;
    }

    void LateUpdate()
    {
        float deltaX = _cameraTransform.position.x - _lastCameraX;
        Vector2 position = transform.position;
        position.x += deltaX * _parallaxFactor;
        transform.position = position;

        _lastCameraX = _cameraTransform.position.x;

        Repeat(cameraDeltaX: deltaX);
    }

    private void Repeat(float cameraDeltaX)
    {
        Vector2 screenPosition = transform.position - _cameraTransform.position;
        Vector2 position = transform.position;

        if (cameraDeltaX > 0)
        {
            // Estoy moviendo el fondo hacia la izquierda
            // Resetear a x original cuando llega al mínimo x
            Debug.Log("pos x: " + screenPosition.x);
            if (screenPosition.x <= _startX - _width)
            {
                position.x = _startX + _cameraTransform.position.x;
            }
        }
        else if (cameraDeltaX < 0)
        {
            // Estoy moviendo el fondo hacia la derecha
            // Resetear a x original cuando llega al máximo x
            if (screenPosition.x >= _startX + _width)
            {
                position.x = _startX + _cameraTransform.position.x;
            }
        }
        transform.position = position;
    }
}
