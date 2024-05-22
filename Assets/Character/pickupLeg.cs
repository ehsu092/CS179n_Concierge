using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Add this to use UI components

public class PickupLeg : MonoBehaviour
{
    public GameObject interactionText;  // Reference to the UI Text element
    // public GameObject legNotPickup;
    // public GameObject legPickup;
    public AudioSource pickupSound;
    public float interactionDistance = 2.0f;  // Distance within which the Character can interact

    private bool isInteractable;
    // private GameObject Character;
    public object switch_scence;
    void Start(){
        switch_scence.SetActive(false);
    }

    private void OnTriggerStay(collider other){
        if(other.gameObject.tag == "MainCamera"){
            if(Input.GetKeyDown(KeyCode.E)){
                this.gameObject.SetActive(false);
                switch_scence.SetActive(true);
            }
        }
    }
    // void Start()
    // {
    //     // Find the Character object by accessing the first person controller
    //     GameObject firstPersonController = GameObject.Find("Character");
    //     if (firstPersonController != null)
    //     {
    //         Character = firstPersonController.transform.GetChild(0).gameObject;
    //         if (Character == null)
    //         {
    //             Debug.LogError("Character not found. Make sure the Character object is a child of the FPSController.");
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("FPSController not found. Make sure the FPSController object is in the scene.");
    //     }

    //     // Hide the interaction text initially
    //     if (interactionText != null)
    //     {
    //         interactionText.SetActive(false);
    //     }
    // }

    // void Update()
    // {
    //     if (isInteractable && Input.GetKeyDown(KeyCode.E))
    //     {
    //         interactionText.SetActive(false);
    //         isInteractable = false;

    //         // Play pickup sound if assigned
    //         if (pickupSound != null)
    //         {
    //             pickupSound.Play();
    //         }

    //         // Toggle leg game objects
    //         if (legPickup != null)
    //         {
    //             legPickup.SetActive(true);
    //         }

    //         if (legNotPickup != null)
    //         {
    //             legNotPickup.SetActive(false);
    //         }
    //     }

    //     // Check the interaction distance
    //     if (IsCharacterClose())
    //     {
    //         if (interactionText != null)
    //         {
    //             interactionText.SetActive(true);
    //             isInteractable = true;
    //         }
    //     }
    //     else
    //     {
    //         if (interactionText != null)
    //         {
    //             interactionText.SetActive(false);
    //             isInteractable = false;
    //         }
    //     }
    // }

    // private bool IsCharacterClose()
    // {
    //     if (Character != null)
    //     {
    //         // Calculate the distance between the Character and the object
    //         float distance = Vector3.Distance(Character.transform.position, transform.position);

    //         // Debug distance for troubleshooting
    //         Debug.Log("Distance to object: " + distance);

    //         // Return true if the distance is within the interaction distance threshold
    //         return distance <= interactionDistance;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // void OnTriggerStay(Collider other)
    // {
    //     // Ensure the Character is within interaction distance
    //     if (other.CompareTag("MainCamera") && IsCharacterClose())
    //     {
    //         Debug.Log("Character is within the trigger area.");
    //         interactionText.SetActive(true);
    //         isInteractable = true;
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("MainCamera"))
    //     {
    //         Debug.Log("Character has exited the trigger area.");
    //         interactionText.SetActive(false);
    //         isInteractable = false;
    //     }
    // }
}
