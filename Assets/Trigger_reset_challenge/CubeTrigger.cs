using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    public pickup_flashlight flashlightScript; // Reference to the flashlight script
    public GameObject teleporter1;
    public GameObject teleporter2;

    private bool hasTriggered = false; // Flag to ensure the trigger happens only once

    void Start()
    {
        if (teleporter1 != null)
        {
            teleporter1.SetActive(false);
        }
        if (teleporter2 != null)
        {
            teleporter2.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Set the flag to prevent multiple triggers
            ResetChallengeAndActivateTeleporters();
        }
    }

    void ResetChallengeAndActivateTeleporters()
    {
        if (flashlightScript != null)
        {
            flashlightScript.challenge = 0; // Reset the challenge count
        }

        if (teleporter1 != null)
        {
            teleporter1.SetActive(true);
        }
        if (teleporter2 != null)
        {
            teleporter2.SetActive(true);
        }
    }
}
