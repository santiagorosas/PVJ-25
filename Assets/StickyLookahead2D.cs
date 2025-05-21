// StickyLookahead2D.cs
using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineCamera))]
public class StickyLookahead2D : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Transform cameraFocus;   // the proxy object
    [SerializeField] private float aheadDistance = 3f;
    [SerializeField] private float minSpeed = 0.05f;

    private int _facing = 1;         // 1 = right, -1 = left

    void LateUpdate()
    {
        Debug.Log("uso");

        // Update facing only if we're really moving
        if (Mathf.Abs(playerRb.linearVelocity.x) > minSpeed)
        {
            _facing = playerRb.linearVelocity.x > 0 ? 1 : -1;
        }

        var p = playerRb.position;
        cameraFocus.position =
            new Vector3(p.x + _facing * aheadDistance, p.y, cameraFocus.position.z);
    }
}
