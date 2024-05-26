using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        // Initialize the ray to start from the object's position and move in its forward direction
        ray = new Ray(transform.position, transform.forward);

        // Start drawing the line from the object's position
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        // Reflect the ray and update the line renderer positions
        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                Debug.Log("Hit: " + hit.collider.name); // Log the name of the hit object

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);

                // Check if the hit object is tagged as "Mirror"
                if (hit.collider.tag == "Mirror")
                {
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                    Debug.Log("Reflected off mirror at: " + hit.point); // Log the reflection point
                }
                else
                {
                    break; // Stop if the object is not a mirror
                }
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
                Debug.Log("Ray ended at: " + (ray.origin + ray.direction * remainingLength)); // Log where the ray ends
                break; // Exit loop if no hit to avoid infinite loop
            }
        }
    }
}
