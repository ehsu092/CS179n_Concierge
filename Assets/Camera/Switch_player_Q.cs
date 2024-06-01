using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPlayerQ : MonoBehaviour
{
    [SerializeField] private Transform currentPlayer; // Reference to the current player
    [SerializeField] private Transform newPlayer; // Reference to the new player
    public Text switchPromptText; // Reference to the UI text for switch prompt
    public GameObject[] objectsToDeactivate; // Array of GameObjects to deactivate
    public GameObject[] objectsToActivate; // Array of GameObjects to activate
    private Transform previousPlayer; // Reference to store the previous player

    void Start()
    {
        // Disable switch prompt text at start
        switchPromptText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check for 'Q' key press to switch players
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Switch players
            SwitchPlayers();

            // Deactivate and activate additional objects
            DeactivateObjects();
            ActivateObjects();

            // currentPlayer = nextPlayer;
        }
    }

    private void SwitchPlayers()
    {
        // If there's no previous player, this is the first switch
        if (previousPlayer == null)
        {
            // Store the current player as the previous player
            previousPlayer = currentPlayer;
        }
        
        // Determine the next player to switch to
        Transform nextPlayer = (currentPlayer == previousPlayer) ? newPlayer : previousPlayer;

        // Disable the current player
        currentPlayer.gameObject.SetActive(false);

        // Enable the next player
        nextPlayer.gameObject.SetActive(true);

        // Update reference to current player
        // currentPlayer = nextPlayer;

        Debug.Log("Player switched!");
    }

    private void DeactivateObjects()
    {
        // Deactivate each object in the objectsToDeactivate array
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }

    private void ActivateObjects()
    {
        // Activate each object in the objectsToActivate array
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
