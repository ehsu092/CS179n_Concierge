using UnityEngine;
using UnityEngine.UI;

public class TriggerCubeScript : MonoBehaviour
{
    public Text interactText;  // Reference to the UI Text component
    public Text moveText;      // Reference to the UI Text component for movement instructions
    private bool playerInZone = false;

    void Start()
    {
        // Ensure the texts are not visible at the start
        interactText.gameObject.SetActive(false);
        moveText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check if the player is in the trigger zone and presses 'E'
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            // Hide the interact text and show the move text
            interactText.gameObject.SetActive(false);
            moveText.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            interactText.gameObject.SetActive(true);  // Show the interact text
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            interactText.gameObject.SetActive(false);  // Hide the interact text
            moveText.gameObject.SetActive(false);      // Hide the move text
        }
    }
}
