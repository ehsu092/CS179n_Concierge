using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import UnityEngine.UI to use Text component

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    public int reflections;
    public float maxLength;
    public GameObject spotlightPrefab; // Reference to the spotlight prefab
    public Text successText; // Text to display when challenge is passed

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private GameObject spotlight; // Reference to the spotlight GameObject

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Instantiate the spotlight GameObject from the prefab
        if (spotlightPrefab != null)
        {
            spotlight = Instantiate(spotlightPrefab);
            spotlight.SetActive(false); // Initially hide the spotlight
        }
    }

    private void LateUpdate()
    {
        bool mirrorHit = false; // Flag to track if a mirror is hit by the ray

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
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                
                Debug.Log("Hit Object: " + hit.collider.gameObject.name + ", Tag: " + hit.collider.tag);
                // Check if the hit object is tagged as "Mirror"
                if (hit.collider.tag == "Mirror")
                {
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                    mirrorHit = true; // Set the flag to true if a mirror is hit
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
                break; // Exit loop if no hit to avoid infinite loop
            }
        }

        // Activate the spotlight only if a mirror is hit by the ray
        if (spotlight != null)
        {
            spotlight.SetActive(mirrorHit); // Activate spotlight if a mirror is hit
            if (mirrorHit)
            {
                spotlight.transform.position = hit.point; // Position at the end point of the ray
                spotlight.transform.rotation = Quaternion.LookRotation(-hit.normal); // Rotate opposite to hit normal
            }
        }

        // Activate the success text when the challenge is passed
        if (mirrorHit && hit.collider.tag == "tv target" && successText != null)
        {
            StartCoroutine(DisplayTextAfterDelay());
        }
    }
    private IEnumerator DisplayTextAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        successText.gameObject.SetActive(true);
    }
}
