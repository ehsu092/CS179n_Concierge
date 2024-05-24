using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public GameObject Gun;
    public Transform weaponParent;
    public float dropForce = 5f; // Customize drop force according to your needs
    public float pickupDistance = 2f; // Distance within which the item can be picked up

    public Text pickupText; // Reference to the UI text element for displaying pickup text

    private bool isPickedUp = false; // Flag to track if the object is picked up

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the gun is initially kinematic
        Gun.GetComponent<Rigidbody>().isKinematic = true;

        // Hide the pickup text initially
        if (pickupText != null)
        {
            pickupText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
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

        // Hide the pickup text
        if (pickupText != null)
        {
            pickupText.gameObject.SetActive(false);
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

        // Hide the pickup text
        if (pickupText != null)
        {
            pickupText.gameObject.SetActive(false);
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
                // Show pickup text
                if (pickupText != null)
                {
                    pickupText.gameObject.SetActive(true);
                }

                // Check for pickup input
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Equip();
                }
            }
            else
            {
                // Hide pickup text if player is not within pickup distance
                if (pickupText != null)
                {
                    pickupText.gameObject.SetActive(false);
                }
            }
        }
    }
}
