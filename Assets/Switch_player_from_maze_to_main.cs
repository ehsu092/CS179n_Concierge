using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(LineRenderer))]
public class PlayerCameraSwitch_from_maze_to_main : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public GameObject targetCube; // The specific cube object
    public Text switchPromptText;
    public float switchDistance = 5f; // Distance within which the switch is allowed

    private Transform currentPlayer;
    private Transform otherPlayer;

    private bool challengePassed = false;
    public Text successText; // Text to display when challenge is passed

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

            if (Input.GetKeyDown(KeyCode.E) && challengePassed == false)
            {
                 IncrementChallenge();

                challengePassed = true;
                
                Debug.Log("E key pressed, switching players.");

                SwitchPlayers();

                StartCoroutine(DisplaySuccessTex(2f));
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

    public void ResetPlayer1ToPosition(float x, float y, float z)
    {
        player1.position = new Vector3(x, y, z);
        player1.gameObject.SetActive(true); // Activate Player 1
    }
    private void IncrementChallenge()
    {
        // Find the pickup_flashlight script and call IncrementChallenge method
        pickup_flashlight pickupScript = FindObjectOfType<pickup_flashlight>();
        if (pickupScript != null)
        {
            pickupScript.IncrementChallenge();
        }
        else
        {
            Debug.LogError("pickup_flashlight script not found.");
        }
    }

    IEnumerator DisplaySuccessTex(float delay)
    {
        // Wait for the initial delay
        yield return new WaitForSeconds(delay);

        // Activate the success text if it exists
        if (successText != null)
        {
            successText.gameObject.SetActive(true);

            // Wait for 3 seconds
            yield return new WaitForSeconds(3f);

            // Deactivate the success text
            successText.gameObject.SetActive(false);
        }
    }
}
