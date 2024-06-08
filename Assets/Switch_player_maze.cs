using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraSwitch_maze : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public GameObject targetCube; // The specific cube object
    public Text switchPromptText;
    public float switchDistance = 5f; // Distance within which the switch is allowed

    private Transform currentPlayer;
    private Transform otherPlayer;

    void Start()
    {
        currentPlayer = player1;
        otherPlayer = player2;

        switchPromptText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check the distance between player1 and the cube
        float distanceToCube = Vector3.Distance(player1.position, targetCube.transform.position);
        Debug.Log("Distance to cube: " + distanceToCube);

        if (currentPlayer == player1 && distanceToCube < switchDistance)
        {
            Debug.Log("Player 1 is within switch distance.");
            switchPromptText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed, switching players.");
                SwitchPlayers();
            }
        }
        else
        {
            switchPromptText.gameObject.SetActive(false);
        }
    }

    void SwitchPlayers()
    {
        currentPlayer.gameObject.SetActive(false);
        otherPlayer.gameObject.SetActive(true);

        // Swap the references
        Transform temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;

        // Update the prompt text visibility
        switchPromptText.gameObject.SetActive(false);
    }
}
