using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public GameObject miniGameCanvas;  // Reference to the Canvas GameObject
    public Transform cube;             // Reference to the Cube Transform
    public Transform player;           // Reference to the Player Transform
    public float interactionDistance = 1.0f;  // Distance within which the player can interact
    private bool isNearCube = false;
    private FPSController fpsController; // Reference to the FPSController script

    void Start()
    {
        if (player == null)
        {
            player = transform; // If player is not assigned, use the transform of the GameObject this script is attached to
        }

        // Find the FPSController component on the player
        fpsController = player.GetComponent<FPSController>();

        // Ensure the mini-game Canvas is initially inactive
        miniGameCanvas.SetActive(false);
    }

    void Update()
    {
        // Check the distance between the player and the cube
        float distance = Vector3.Distance(player.position, cube.position);

        if (distance <= interactionDistance)
        {
            isNearCube = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Enable the mini-game canvas
                miniGameCanvas.SetActive(true);

                // Disable player movement
                fpsController.canMove = false;

                // Unlock and show the cursor for the mini-game
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            isNearCube = false;
        }

        // Optionally, you can close the mini-game by pressing the Escape key
        if (miniGameCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            miniGameCanvas.SetActive(false);

            // Re-enable player movement
            fpsController.canMove = true;

            // Lock and hide the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnGUI()
    {
        if (isNearCube && !miniGameCanvas.activeSelf)
        {
            // Display a message when the player is near the cube and the mini-game is not active
            GUI.Label(new Rect(10, 10, 200, 20), "Press 'E' to play the mini-game");
        }
    }
}
