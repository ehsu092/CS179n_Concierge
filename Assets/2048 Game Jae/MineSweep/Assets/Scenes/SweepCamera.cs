using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // Reference to the player
    public Vector3 offset;    // Offset value for the camera

    void LateUpdate()
    {
        // Update the camera's position
        transform.position = target.position + offset;
    }
}
