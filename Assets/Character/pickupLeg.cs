// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;  // Add this to use UI components

// public class PickupLeg : MonoBehaviour
// {
//     public GameObject interactionText;  // Reference to the UI Text element
//     public GameObject legNotPickup;     // Reference to the leg object before pickup
//     public GameObject legPickup;        // Reference to the leg object after pickup
//     public AudioSource pickupSound;     // Reference to the pickup sound
//     public float interactionDistance = 2.0f;  // Distance within which the Character can interact

//     private bool isInteractable;
//     private GameObject character;

//     void Start()
//     {
//         // Find the Character object by accessing the first person controller
//         GameObject firstPersonController = GameObject.Find("Character");
//         if (firstPersonController != null)
//         {
//             character = firstPersonController.transform.GetChild(0).gameObject;
//             if (character == null)
//             {
//                 Debug.LogError("Character not found. Make sure the Character object is a child of the FPSController.");
//             }
//         }
//         else
//         {
//             Debug.LogError("FPSController not found. Make sure the FPSController object is in the scene.");
//         }

//         // Hide the interaction text initially
//         if (interactionText != null)
//         {
//             interactionText.SetActive(false);
//         }
//     }

//     void Update()
//     {
//         if (character == null)
//         {
//             return;
//         }

//         // Check the interaction distance
//         if (IsCharacterClose())
//         {
//             if (interactionText != null)
//             {
//                 interactionText.SetActive(true);
//                 isInteractable = true;
//             }

//             if (Input.GetKeyDown(KeyCode.E) && isInteractable)
//             {
//                 interactionText.SetActive(false);
//                 isInteractable = false;

//                 // Play pickup sound if assigned
//                 if (pickupSound != null)
//                 {
//                     pickupSound.Play();
//                 }

//                 // Toggle leg game objects
//                 if (legPickup != null)
//                 {
//                     legPickup.SetActive(true);
//                 }

//                 if (legNotPickup != null)
//                 {
//                     legNotPickup.SetActive(false);
//                 }

//                 // Deactivate this object
//                 gameObject.SetActive(false);
//             }
//         }
//         else
//         {
//             if (interactionText != null)
//             {
//                 interactionText.SetActive(false);
//                 isInteractable = false;
//             }
//         }
//     }

//     private bool IsCharacterClose()
//     {
//         if (character != null)
//         {
//             // Calculate the distance between the character and the object
//             float distance = Vector3.Distance(character.transform.position, transform.position);

//             // Debug distance for troubleshooting
//             Debug.Log("Distance to object: " + distance);

//             // Return true if the distance is within the interaction distance threshold
//             return distance <= interactionDistance;
//         }
//         else
//         {
//             return false;
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Add this to use UI components

public class PickupLeg : MonoBehaviour
{
    public GameObject interactionText;  // Reference to the UI Text element
    public GameObject legNotPickup;     // Reference to the leg object before pickup
    public GameObject legPickup;        // Reference to the leg object after pickup
    public AudioSource pickupSound;     // Reference to the pickup sound
    public float interactionDistance = 2.0f;  // Distance within which the Character can interact

    private bool isInteractable;
    private bool hasLeg;  // Track if the player has picked up the leg
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

        // Ensure legPickup is initially hidden
        if (legPickup != null)
        {
            legPickup.SetActive(false);
        }
    }

    void Update()
    {
        if (character == null)
        {
            return;
        }

        if (hasLeg)
        {
            // Drop the leg if the player presses E again
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Specify the drop position here (e.g., character's position plus a small offset forward)
                Vector3 dropPosition = character.transform.position + character.transform.forward + Vector3.up * 0.5f; // slightly above ground
                DropLeg(dropPosition);
            }
        }
        else
        {
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

                    // Indicate the player has picked up the leg
                    hasLeg = true;
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

    private void DropLeg(Vector3 dropPosition)
    {
        // Ensure legNotPickup is active
        if (legNotPickup != null)
        {
            legNotPickup.SetActive(true);
        }

        // Hide the legPickup object since it was deactivated
        if (legPickup != null)
        {
            legPickup.SetActive(false);
        }

        // Move the leg object to the specified drop position
        transform.position = dropPosition;

        // Indicate the player no longer has the leg
        hasLeg = false;
    }
}
