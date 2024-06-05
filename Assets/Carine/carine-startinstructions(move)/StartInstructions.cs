using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInstructions : MonoBehaviour
{
    public GameObject instructionsCanvas; // Reference to the InstructionsCanvas GameObject

    private bool hasTriggered = false; // A flag to ensure the trigger activates only once

    void Start()
    {
        instructionsCanvas.SetActive(false); // Make the text invisible at first
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Set the flag to true to prevent re-triggering
            instructionsCanvas.SetActive(true); // Display the text
            StartCoroutine(WaitForSec()); // Start the coroutine to hide the text after a delay
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(7); // Wait for 7 seconds
        instructionsCanvas.SetActive(false); // Hide the text
        // Destroy(gameObject); // Optional: Destroy the trigger object if you don't need it anymore
    }
}