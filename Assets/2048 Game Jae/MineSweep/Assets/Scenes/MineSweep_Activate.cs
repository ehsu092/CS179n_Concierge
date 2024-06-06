using UnityEngine;
using KinematicCharacterController.Examples;

public class GridGameTrigger : MonoBehaviour
{
    public GameObject gridGameCanvas;  // Reference to the Canvas containing the grid game
    public Transform player;           // Reference to the Player Transform
    public float interactionDistance = 3.0f;  // Distance within which the player can interact
    private bool isNearCube = false;

    private ExampleCharacterCamera cameraController;

    void Start()
    {
        // Ensure the grid-based game Canvas is initially inactive
        gridGameCanvas.SetActive(false);

        // Ensure the cursor is locked and hidden initially
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get reference to the ExampleCharacterCamera script
        cameraController = Camera.main.GetComponent<ExampleCharacterCamera>();
        if (cameraController == null)
        {
            Debug.LogError("ExampleCharacterCamera script not found on the main camera.");
        }
    }

    void Update()
    {
        // Check the distance between the player and the cube
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionDistance)
        {
            isNearCube = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Enable the grid-based game Canvas
                gridGameCanvas.SetActive(true);

                // Unlock and show the cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Optionally, disable other game controls/scripts
                DisableOtherControls();
            }
        }
        else
        {
            isNearCube = false;
        }

        // Optionally, you can close the grid-based game by pressing the Escape key
        if (gridGameCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            gridGameCanvas.SetActive(false);

            // Lock and hide the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Re-enable other game controls/scripts
            EnableOtherControls();
        }
    }

    void OnGUI()
    {
        if (isNearCube && !gridGameCanvas.activeSelf)
        {
            // Display a message when the player is near the cube and the grid game is not active
            GUI.Label(new Rect(10, 10, 300, 20), "Press 'E' to play the grid-based game");
        }
    }

    void DisableOtherControls()
    {
        // Disable the camera control script
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }
    }

    void EnableOtherControls()
    {
        // Re-enable the camera control script
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }
    }
}
