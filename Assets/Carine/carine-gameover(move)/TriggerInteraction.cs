using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TriggerInteraction : MonoBehaviour
{
    public Canvas canvas;  // Reference to the Canvas
    public Button playAgainButton;  // Reference to the Play Again button
    public Button quitButton;  // Reference to the Quit button

    private int selectedIndex = 0;  // To keep track of the selected option
    private Button[] buttons;

    private void Start()
    {
        // Ensure the canvas is initially hidden
        canvas.gameObject.SetActive(false);

        // Add listeners to the buttons
        playAgainButton.onClick.AddListener(PlayAgain);
        quitButton.onClick.AddListener(QuitGame);

        // Store the buttons in an array for easy navigation
        buttons = new Button[] { playAgainButton, quitButton };

        // Set the initial button as selected
        EventSystem.current.SetSelectedGameObject(buttons[selectedIndex].gameObject);
    }

    private void Update()
    {
        if (canvas.gameObject.activeSelf)
        {
            HandleKeyboardInput();
        }
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[selectedIndex].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % buttons.Length;
            EventSystem.current.SetSelectedGameObject(buttons[selectedIndex].gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].onClick.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the canvas when the player enters the trigger
            canvas.gameObject.SetActive(true);
            // Set the initial button as selected
            EventSystem.current.SetSelectedGameObject(buttons[selectedIndex].gameObject);
            // Optionally, pause the game or disable player controls here
            Time.timeScale = 0;  // This pauses the game
        }
    }

    public void PlayAgain()
    {
        // Hide the canvas
        canvas.gameObject.SetActive(false);
        // Unpause the game
        Time.timeScale = 1;
        // Optionally, reset the game state here
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // Note: Application.Quit() does not work in the editor. Use the following line for testing in the editor:
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}