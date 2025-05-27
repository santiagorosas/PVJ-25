using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    private Camera _camera;
    private float _previousCameraX;

    [SerializeField] float _parallaxFactor;


    void Start()
    {
        _camera = Camera.main;
    }

    
    void Update()
    {
        float cameraDeltaX = _camera.transform.position.x - _previousCameraX;

        Vector2 position = transform.position;
        position.x -= cameraDeltaX * _parallaxFactor;
        transform.position = position;

        _previousCameraX = _camera.transform.position.x;
    }
}
