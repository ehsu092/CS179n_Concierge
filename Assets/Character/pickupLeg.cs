using UnityEngine;

public class PickupLeg : MonoBehaviour
{
    public GameObject interactionText;  // Reference to the UI Text element
    public GameObject legNotPickup;     // Reference to the leg object before pickup
    public GameObject legPickup;        // Reference to the leg object after pickup
    public AudioSource pickupSound;     // Reference to the pickup sound
    public float interactionDistance = 2.0f;  // Distance within which the Character can interact

    private bool isInteractable;
    private GameObject character;

    void Start()
    {
        // Find the Character object by accessing the first person controller
        GameObject firstPersonController = GameObject.Find("Character");
        if (firstPersonController != null)
        {
            character = firstPersonController.transform.GetChild(0).gameObject;
            if (character == null)
            {
                Debug.LogError("Character not found. Make sure the Character object is a child of the FPSController.");
            }
        }
        else
        {
            Debug.LogError("FPSController not found. Make sure the FPSController object is in the scene.");
        }

        // Hide the interaction text initially
        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
    }

    void Update()
    {
        if (character == null)
        {
            return;
        }

        // Check the interaction distance
        if (IsCharacterClose())
        {
            if (interactionText != null)
            {
                interactionText.SetActive(true);
                isInteractable = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && isInteractable)
            {
                interactionText.SetActive(false);
                isInteractable = false;

                // Play pickup sound if assigned
                if (pickupSound != null)
                {
                    pickupSound.Play();
                }

                // Toggle leg game objects
                if (legPickup != null)
                {
                    legPickup.SetActive(true);
                }

                if (legNotPickup != null)
                {
                    legNotPickup.SetActive(false);
                }

                // Deactivate this object
                gameObject.SetActive(false);
            }

            // Check if the leg is being carried and drop if Q is pressed
            if (Input.GetKeyDown(KeyCode.Q) && legPickup.activeSelf)
            {
                Drop();
            }
        }
        else
        {
            if (interactionText != null)
            {
                interactionText.SetActive(false);
                isInteractable = false;
            }
        }
    }

    private void Drop()
    {
        // Ensure the "Bottoms" object is found and camera is valid
        GameObject bottoms = GameObject.Find("Bottoms");
        if (bottoms != null && Camera.main != null)
        {
            // Set the position of the "Bottoms" object to the camera's position
            bottoms.transform.position = Camera.main.transform.position;

            // Reactivate this object
            gameObject.SetActive(true);

            // Toggle leg game objects
            if (legPickup != null)
            {
                legPickup.SetActive(false);
            }

            if (legNotPickup != null)
            {
                legNotPickup.SetActive(true);
            }
        }
    }

    private bool IsCharacterClose()
    {
        if (character != null)
        {
            // Calculate the distance between the character and the object
            float distance = Vector3.Distance(character.transform.position, transform.position);

            // Debug distance for troubleshooting
            Debug.Log("Distance to object: " + distance);

            // Return true if the distance is within the interaction distance threshold
            return distance <= interactionDistance;
        }
        else
        {
            return false;
        }
    }
}
