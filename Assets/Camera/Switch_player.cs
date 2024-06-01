using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraSwitch : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public GameObject cameraObject;
    public GameObject[] player1ObjectsToDeactivate; // Array of GameObjects to deactivate for player 1
    public GameObject[] player1ObjectsToActivate;   // Array of GameObjects to activate for player 1
    public GameObject[] player2ObjectsToDeactivate; // Array of GameObjects to deactivate for player 2
    public GameObject[] player2ObjectsToActivate;   // Array of GameObjects to activate for player 2
    public Text switchPromptText; // Reference to the UI text for switch prompt
    public Text[] player2UITexts; // Array of UI texts for player 2
    public float displayDuration = 3f; // Duration for which UI text is displayed

    private Transform currentPlayer;
    private Transform otherPlayer;
    private bool player2UITextsDisplayed;

    void Start()
    {
        // Initialize the current player as player 1 and the other player as player 2
        currentPlayer = player1;
        otherPlayer = player2;

        // Disable switch prompt text at start
        switchPromptText.gameObject.SetActive(false);

        // Disable player 2 UI texts at start
        foreach (Text text in player2UITexts)
        {
            text.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Calculate the distance between player 1 and the camera
        float distanceToCamera = Vector3.Distance(player1.position, cameraObject.transform.position);
        Debug.Log("Distance to camera: " + distanceToCamera);

        // Check if player 1 is close enough to the camera to display the switch prompt text
        if (currentPlayer == player1 && distanceToCamera < 5f)
        {
            // Display switch prompt text
            switchPromptText.gameObject.SetActive(true);
        }
        else
        {
            // Hide switch prompt text if player 1 is far from the camera or if player 2 is active
            switchPromptText.gameObject.SetActive(false);
        }

        // Check for 'E' key press to switch players
        if (Input.GetKeyDown(KeyCode.E) && currentPlayer == player1 && distanceToCamera < 5f)
        {
            SwitchPlayers();
            DeactivateCamera();
            DeactivateObjects(currentPlayer == player1 ? player1ObjectsToDeactivate : player2ObjectsToDeactivate);
            ActivateObjects(currentPlayer == player1 ? player1ObjectsToActivate : player2ObjectsToActivate);
        }

        // Check for 'Q' key press to switch back to the original player and activate the camera
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchBackToOriginalPlayer();
            ActivateCamera();
            DeactivateObjects(currentPlayer == player1 ? player1ObjectsToDeactivate : player2ObjectsToDeactivate);
            ActivateObjects(currentPlayer == player1 ? player1ObjectsToActivate : player2ObjectsToActivate);
        }

        // Check if the current player is player 2 and if the UI texts for player 2 have not been displayed yet
        if (currentPlayer == player2 && !player2UITextsDisplayed)
        {
            StartCoroutine(DisplayUITextsInOrder());
            player2UITextsDisplayed = true; // Set the flag to true to indicate that UI texts have been displayed
        }
    }

    void SwitchPlayers()
    {
        // Disable the current player
        currentPlayer.gameObject.SetActive(false);

        // Enable the other player
        otherPlayer.gameObject.SetActive(true);

        // Swap the current player and the other player
        Transform temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;
    }

    void SwitchBackToOriginalPlayer()
    {
        // Disable the current player
        currentPlayer.gameObject.SetActive(false);

        // Enable the other player
        otherPlayer.gameObject.SetActive(true);

        // Swap the current player and the other player
        Transform temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;
    }

    void DeactivateCamera()
    {
        // Deactivate the camera object
        cameraObject.SetActive(false);
    }

    void ActivateCamera()
    {
        // Activate the camera object
        cameraObject.SetActive(true);
    }

    void DeactivateObjects(GameObject[] objects)
    {
        // Deactivate each object in the objects array
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    void ActivateObjects(GameObject[] objects)
    {
        // Activate each object in the objects array
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

    IEnumerator DisplayUITextsInOrder()
    {
        // Display each UI text for player 2 for a certain duration
        foreach (Text text in player2UITexts)
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(displayDuration);
            text.gameObject.SetActive(false);
        }
    }
}
