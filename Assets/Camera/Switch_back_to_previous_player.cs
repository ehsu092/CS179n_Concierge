using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch_back_to_previous_player : MonoBehaviour
{
    public Transform currentPlayer; // Reference to the current player
    public Transform originalPlayer; // Reference to the original player
    public GameObject[] objectsToDeactivate; // Array of GameObjects to deactivate
    public GameObject[] objectsToActivate; // Array of GameObjects to activate
    public Text[] switchTexts; // Array of UI texts for switch prompt
    public float displayDuration = 3f; // Duration for which each UI text is displayed
    
    private Transform temperature_player;
    void Start()
    {
        // Set the original player as the current player at the start
        originalPlayer = currentPlayer;
    }

    void Update()
    {
        // Check for 'Q' key press to switch back to original player
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Switch back to original player
            SwitchToOriginalPlayer();

            // Deactivate and activate additional objects
            DeactivateObjects();
            ActivateObjects();

            // Display UI texts in order for a certain duration
            StartCoroutine(DisplayUITextsInOrder());
        }
    }

    void SwitchToOriginalPlayer()
    {
        // Disable the current player
        currentPlayer.gameObject.SetActive(false);

        // Enable the original player
        originalPlayer.gameObject.SetActive(true);
        
        // Store the current player reference temporarily
        temperature_player = currentPlayer;

        // Update reference to current player
        currentPlayer = originalPlayer;

        // Update reference to original player using the temporary reference
        originalPlayer = temperature_player;

        // Reset the temporary reference
        temperature_player = null;

        Debug.Log("Switched back to original player!");
    }

    void DeactivateObjects()
    {
        // Deactivate each object in the objectsToDeactivate array
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }

    void ActivateObjects()
    {
        // Activate each object in the objectsToActivate array
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    IEnumerator DisplayUITextsInOrder()
    {
        // Display each UI text in order for a certain duration
        foreach (Text text in switchTexts)
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(displayDuration);
            text.gameObject.SetActive(false);
        }
    }
}
