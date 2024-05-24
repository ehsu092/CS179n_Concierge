using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Required for using Coroutines

public class Pickup : MonoBehaviour
{
    public GameObject Gun;
    public Transform weaponParent;
    public float dropForce = 5f; // Customize drop force according to your needs
    public float pickupDistance = 2f; // Distance within which the item can be picked up
    public float inputX; // X coordinate for the drop challenge position
    public float inputY; // Y coordinate for the drop challenge position
    public float inputZ; // Z coordinate for the drop challenge position
    public float challengeThreshold = 0.7f; // Threshold to determine if challenge is passed (70% accuracy)
    public Text successText; // Text to display when challenge is passed
    public Text pickupPromptText; // Text to prompt for pickup

    private bool isPickedUp = false; // Flag to track if the object is picked up
    private int challenge = 0; // Challenge count, initialized to 0
    private GameObject bottoms; // Reference to the "Bottoms" object

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the gun is initially kinematic
        Gun.GetComponent<Rigidbody>().isKinematic = true;

        // Disable success text initially
        if (successText != null)
        {
            successText.gameObject.SetActive(false);
        }

        // Disable pickup prompt text initially
        if (pickupPromptText != null)
        {
            pickupPromptText.gameObject.SetActive(false);
        }

        // Find the "Bottoms" object in the scene
        bottoms = GameObject.Find("Bottoms");

        if (bottoms == null)
        {
            Debug.LogError("Bottoms object not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }

        // Output challenge status
        Debug.Log("Challenge Count: " + challenge);
    }

    void Drop()
    {
        // Detach the gun from its parent
        Gun.transform.parent = null;

        // Calculate the drop direction (you can customize this)
        Vector3 dropDirection = transform.forward;

        // Apply a force to simulate a drop
        Rigidbody gunRigidbody = Gun.GetComponent<Rigidbody>();
        gunRigidbody.isKinematic = false;
        gunRigidbody.velocity = dropDirection * dropForce; // Customize dropForce according to your needs

        // Enable the mesh collider
        Gun.GetComponent<MeshCollider>().enabled = true;

        // Reset the picked up flag
        isPickedUp = false;

        if (bottoms != null)
        {
            // Get the position of the "Bottoms" object
            Vector3 bottomsPosition = bottoms.transform.position;
            // Provided challenge position from input
            Vector3 challengePosition = new Vector3(inputX, inputY, inputZ);
            // Distance between dropped position and "Bottoms" position
            float distanceToChallenge = Vector3.Distance(Gun.transform.position, challengePosition);
            // Tolerance range based on challenge threshold
            float allowedDistance = challengeThreshold * pickupDistance;

            if (distanceToChallenge <= allowedDistance)
            {
                // Increase the challenge count
                challenge++;

                // Start coroutine to display success text after 5 seconds
                StartCoroutine(DisplaySuccessTextWithDelay(2f));
            }
        }
    }

    void Equip()
    {
        // Set the gun's position and rotation to the weapon parent's position and rotation
        Gun.transform.position = weaponParent.transform.position;
        Gun.transform.rotation = weaponParent.transform.rotation;

        // Make the gun kinematic
        Gun.GetComponent<Rigidbody>().isKinematic = true;

        // Disable the mesh collider
        Gun.GetComponent<MeshCollider>().enabled = false;

        // Set the weapon parent as the gun's parent
        Gun.transform.SetParent(weaponParent);

        // Set the picked up flag
        isPickedUp = true;

        // Disable pickup prompt text
        if (pickupPromptText != null)
        {
            pickupPromptText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isPickedUp && other.gameObject.CompareTag("Player"))
        {
            // Check if the player is within the pickup distance
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= pickupDistance)
            {
                // Display pickup prompt text
                if (pickupPromptText != null)
                {
                    pickupPromptText.gameObject.SetActive(true);
                }

                // Check for pickup input
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Equip();
                }
            }
            else
            {
                // Hide pickup prompt text if player is not within pickup distance
                if (pickupPromptText != null)
                {
                    pickupPromptText.gameObject.SetActive(false);
                }
            }
        }
    }

    // Coroutine to display success text after a delay
    IEnumerator DisplaySuccessTextWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (successText != null)
        {
            successText.gameObject.SetActive(true);
        }
    }
}
