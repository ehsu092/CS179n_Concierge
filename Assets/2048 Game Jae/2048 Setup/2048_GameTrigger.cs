using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public GameObject miniGameCanvas;  // Reference to the Canvas GameObject
    public Transform cube;             // Reference to the Cube Transform
    public Transform player;           // Reference to the Player Transform
    public float interactionDistance = 3.0f;  // Distance within which the player can interact
    private bool isNearCube = false;

    void Start()
    {
        if (player == null)
        {
            player = transform; // If player is not assigned, use the transform of the GameObject this script is attached to
        }

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
        }
    }

    void OnGUI()
    {
        if (isNearCube)
        {
            // Display a message when the player is near the cube
            GUI.Label(new Rect(10, 10, 200, 20), "Press 'E' to play the mini-game");
        }
    }
}
