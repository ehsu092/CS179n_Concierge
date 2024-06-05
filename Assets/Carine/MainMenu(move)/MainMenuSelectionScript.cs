using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private Button selectedButton;

    void Start()
    {
        // Set the initial selected button to the playButton
        SelectButton(playButton);
    }

    void Update()
    {
        // Check for up and down arrow key presses
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedButton == playButton)
            {
                SelectButton(quitButton);
            }
            else if (selectedButton == quitButton)
            {
                SelectButton(playButton);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedButton == playButton)
            {
                SelectButton(quitButton);
            }
            else if (selectedButton == quitButton)
            {
                SelectButton(playButton);
            }
        }

        // Check for Enter key press to click the selected button
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Ensure the button is selected and interactable before invoking the click
            if (selectedButton != null && selectedButton.interactable)
            {
                selectedButton.onClick.Invoke();
            }
        }
    }

    void SelectButton(Button button)
    {
        selectedButton = button;
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
    }
}