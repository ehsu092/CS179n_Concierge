using UnityEngine;

public class GridGameTrigger : MonoBehaviour
{
    public GameObject gridGame;  // Reference to the Grid GameObject
    public Transform cube;       // Reference to the Cube Transform
    public Transform player;     // Reference to the Player Transform
    public float interactionDistance = 3.0f;  // Distance within which the player can interact
    private bool isNearCube = false;

    void Start()
    {
        if (player == null)
        {
            player = transform; // If player is not assigned, use the transform of the GameObject this script is attached to
        }

        // Ensure the grid-based game is initially inactive
        gridGame.SetActive(false);
        // Ensure the cursor is locked and hidden initially
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                // Enable the grid-based game
                gridGame.SetActive(true);
                // Unlock and show the cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else
        {
            isNearCube = false;
        }

        // Optionally, you can close the grid-based game by pressing the Escape key
        if (gridGame.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            gridGame.SetActive(false);
            // Lock and hide the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnGUI()
    {
        if (isNearCube && !gridGame.activeSelf)
        {
            // Display a message when the player is near the cube and the grid game is not active
            GUI.Label(new Rect(10, 10, 300, 20), "Press 'E' to play the grid-based game");
        }
    }
}
