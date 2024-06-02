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

    // Audio properties
    public AudioSource audioSource;
    public AudioClip openClip;
    public AudioClip closeClip;

    private bool isMoving = false;

    void Start()
    {
        // Find the player object by accessing the first person controller
        GameObject firstPersonController = GameObject.Find("Player");
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

        // Get the AudioSource component
        if (audioSource == null)
        {
            audioSource = door.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource not found on the door object.");
            }
        }

        // Deactivate the AudioSource initially
        if (audioSource != null)
        {
            audioSource.enabled = false;
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
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
        else
        {
            if (currentRot.y > closeRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRot, currentRot.z), speed * Time.deltaTime);
                isMoving = true;
            }
            else
            {
                isMoving = false;
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

        // Play the appropriate sound
        if (audioSource != null)
        {
            audioSource.enabled = true;
            if (opening && openClip != null)
            {
                audioSource.clip = openClip;
                audioSource.Play();
                StartCoroutine(DeactivateAudioAfterPlaying(openClip.length));
            }
            else if (!opening && closeClip != null)
            {
                audioSource.clip = closeClip;
                audioSource.Play();
                StartCoroutine(DeactivateAudioAfterPlaying(closeClip.length));
            }
        }
    }

    private bool IsPlayerClose()
    {
        if (player != null)
        {
            // Calculate the distance between the player and the door
            float distance = Vector3.Distance(player.transform.position, door.transform.position);

            // Debug distance for troubleshooting
            // Debug.Log("Distance to door: " + distance);

            // Return true if the distance is within the interaction distance threshold
            return distance <= interactionDistance;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator DeactivateAudioAfterPlaying(float duration)
    {
        yield return new WaitForSeconds(duration);
        audioSource.enabled = false;
    }
}
