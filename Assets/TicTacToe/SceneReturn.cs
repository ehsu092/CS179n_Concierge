using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class SceneReturn : MonoBehaviour
{

    private bool challengePassed = false;

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(0);
        IncrementChallenge();
        challengePassed = true;
    }
    private void IncrementChallenge()
    {
        // Find the pickup_flashlight script and call IncrementChallenge method
        pickup_flashlight pickupScript = FindObjectOfType<pickup_flashlight>();
        if (pickupScript != null)
        {
            pickupScript.IncrementChallenge();
        }
        else
        {
            Debug.LogError("pickup_flashlight script not found.");
        }
    }
}

