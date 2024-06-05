using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChessPlayerManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Chess_player;
    public GameObject RookStart;
    public GameObject RookFinish;
    public Text successText;
    public Text switchPromptText; // Text to prompt for switching
    public Vector3 goalPosition; // Set the goal position in the Inspector
    public float switchDistance = 2f; // Distance within which the player can switch
    public float challengeThreshold = 0.1f; // Tolerance for reaching the goal position

    private bool isChessPlayerActive = false; // Start as Player by default
    private bool challengePassed = false;
    private bool interactionDone = false; // Ensure interaction only happens once

    void Start()
    {
        // Make sure the success text is initially disabled
        if (successText != null)
        {
            successText.gameObject.SetActive(false);
        }

        // Make sure the switch prompt text is initially disabled
        if (switchPromptText != null)
        {
            switchPromptText.gameObject.SetActive(false);
        }

        // Ensure the initial state is with Player and RookStart active, Chess_player and RookFinish inactive
        Player.SetActive(true);
        Chess_player.SetActive(false);
        RookStart.SetActive(true);
        RookFinish.SetActive(false);
    }

    void Update()
    {
        if (interactionDone)
            return;

        // Check distance between Player and Chess_player
        float distance = Vector3.Distance(Player.transform.position, Chess_player.transform.position);

        if (distance <= switchDistance && !isChessPlayerActive)
        {
            ShowSwitchPrompt();

            // Check for switch input
            if (Input.GetKeyDown(KeyCode.E))
            {
                ActivateChessPlayer();
                HideSwitchPrompt();
            }
        }
        else if (isChessPlayerActive)
        {
            HideSwitchPrompt();
        }

        // Check if the Chess_player has reached the goal position
        if (isChessPlayerActive && Vector3.Distance(Chess_player.transform.position, goalPosition) <= challengeThreshold)
        {
            if (!challengePassed)
            {
                StartCoroutine(WaitAndReactivatePlayer(3f));
                challengePassed = true;
            }
        }
    }

    void ActivateChessPlayer()
    {
        Player.SetActive(false);
        Chess_player.SetActive(true);
        RookStart.SetActive(false);
        isChessPlayerActive = true;
    }

    void DeactivateChessPlayer()
    {
        Player.SetActive(true);
        Chess_player.SetActive(false);
        RookFinish.SetActive(true);
        isChessPlayerActive = false;
        interactionDone = true; // Mark interaction as done
    }

    void ShowSwitchPrompt()
    {
        if (switchPromptText != null)
        {
            switchPromptText.gameObject.SetActive(true);
            switchPromptText.text = "Press E to switch";
        }
    }

    void HideSwitchPrompt()
    {
        if (switchPromptText != null)
        {
            switchPromptText.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitAndReactivatePlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (successText != null)
        {
            successText.gameObject.SetActive(true);

            // Wait for 3 seconds to show success text
            yield return new WaitForSeconds(3f);

            successText.gameObject.SetActive(false);
        }

        DeactivateChessPlayer();
        challengePassed = false;
    }
}
