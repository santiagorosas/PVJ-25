using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform[] backgrounds;
    public float[] parallaxSpeeds;

    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cam.position - previousCamPos;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += deltaMovement * parallaxSpeeds[i];

            Debug.Log($"background {backgrounds[i].name} speed {parallaxSpeeds[i]}");
        }

        previousCamPos = cam.position;
    }
}