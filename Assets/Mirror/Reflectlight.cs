using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLight : MonoBehaviour
{
    public Transform mirrorSurface;
    public Camera reflectionCamera;
    public Light flashlight;
    public Light bouncedBackLight;

    void Update()
    {
        if (mirrorSurface != null && reflectionCamera != null && flashlight != null && bouncedBackLight != null)
        {
            // Calculate the reflection direction for the flashlight
            Vector3 lightDirection = (flashlight.transform.position - mirrorSurface.position).normalized;
            Vector3 reflectionDirection = Vector3.Reflect(-lightDirection, mirrorSurface.forward);

            // Update the reflection camera's position and rotation
            reflectionCamera.transform.position = mirrorSurface.position;
            reflectionCamera.transform.rotation = Quaternion.LookRotation(reflectionDirection, mirrorSurface.up);

            // Activate the bounced-back light
            bouncedBackLight.gameObject.SetActive(true);
        }
    }
}