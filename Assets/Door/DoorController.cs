using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Add this to use UI components

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openRot, closeRot, speed;
    public bool opening;

    public float interactionDistance = 2.0f;

    private GameObject player;
    public Text interactionText;  // Reference to the UI Text element

    void Start()
    {
        // Find the player object by accessing the first person controller
        GameObject firstPersonController = GameObject.Find("Character");
        if (firstPersonController != null)
        {
            player = firstPersonController.transform.GetChild(0).gameObject;
            if (player == null)
            {
                Debug.LogError("Player not found. Make sure the player object is a child of the FPSController.");
            }
        }
        else
        {
            Debug.LogError("FPSController not found. Make sure the FPSController object is in the scene.");
        }

        // Hide the interaction text initially
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerClose())
        {
            // Toggle the door state when 'E' is pressed
            ToggleDoor();
        }

        Vector3 currentRot = door.transform.localEulerAngles;
        if (opening)
        {
            if (currentRot.y < openRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed * Time.deltaTime);
            }
        }
        else
        {
            if (currentRot.y > closeRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRot, currentRot.z), speed * Time.deltaTime);
            }
        }

        // Show or hide the interaction text based on the player's distance to the door
        if (IsPlayerClose())
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
            }
        }
        else
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleDoor()
    {
        opening = !opening;
    }

    private bool IsPlayerClose()
    {
        if (player != null)
        {
            // Calculate the distance between the player and the door
            float distance = Vector3.Distance(player.transform.position, door.transform.position);

            // Debug distance for troubleshooting
            Debug.Log("Distance to door: " + distance);

            // Return true if the distance is within the interaction distance threshold
            return distance <= interactionDistance;
        }
        else
        {
            return false;
        }
    }
}
