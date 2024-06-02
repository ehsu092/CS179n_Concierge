using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(LineRenderer))]

public class PlayerCameraSwitch : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public GameObject cameraObject;
    public GameObject[] player1ObjectsToDeactivate;
    public GameObject[] player1ObjectsToActivate;
    public GameObject[] player2ObjectsToDeactivate;
    public GameObject[] player2ObjectsToActivate;
    public Text switchPromptText;
    public Text[] player2UITexts;
    public float displayDuration = 3f;
    public Light flashLight; // Reference to the light component
    public float flashDuration = 0.1f; // Duration of the light flash

    public RawImage photoDisplayImage;

    public Text passchallenge;

    // Manual input for challenge location
    public float challengeLocationX;
    public float challengeLocationY;
    public float challengeLocationZ;
    private Vector3 challengeLocation;
    private Transform currentPlayer;
    private Transform otherPlayer;
    private bool player2UITextsDisplayed;
    private bool takingScreenshot = false; // Flag to indicate if a screenshot is being taken
    private bool challengePassed = false;
    void Start()
    {
        // Ensure the flash light is initially deactivated
        flashLight.gameObject.SetActive(false);


        currentPlayer = player1;
        otherPlayer = player2;

        switchPromptText.gameObject.SetActive(false);

        foreach (Text text in player2UITexts)
        {
            text.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Ensure the flash light is initially deactivated
        flashLight.gameObject.SetActive(false);

        float distanceToCamera = Vector3.Distance(player1.position, cameraObject.transform.position);
        Debug.Log("Distance to camera: " + distanceToCamera);

        if (currentPlayer == player1 && distanceToCamera < 5f)
        {
            switchPromptText.gameObject.SetActive(true);
        }
        else
        {
            switchPromptText.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && currentPlayer == player1 && distanceToCamera < 5f)
        {
            // Ensure the flash light is initially deactivated
            flashLight.gameObject.SetActive(false);

            SwitchPlayers();
            DeactivateCamera();
            HandleObjectsActivation();
        }

        if (Input.GetKeyDown(KeyCode.Q) && currentPlayer == player2)
        {
            // Ensure the flash light is initially deactivated
            flashLight.gameObject.SetActive(false);
            
            SwitchBackToOriginalPlayer();
            ActivateCamera();
            HandleObjectsActivation();
        }

        if (currentPlayer == player2 && !player2UITextsDisplayed)
        {
            // Ensure the flash light is initially deactivated
            flashLight.gameObject.SetActive(false);

            StartCoroutine(DisplayUITextsInOrder());
            player2UITextsDisplayed = true;
        }

        // if (Input.GetKeyDown(KeyCode.E) && currentPlayer == player2 && !takingScreenshot)
        // {
        //     // Ensure the flash light is initially deactivated
        //     flashLight.gameObject.SetActive(false);
            
        //     takingScreenshot = true;

        //     StartCoroutine(CaptureSceneWithFlash());
        // }
        if (Input.GetKeyDown(KeyCode.E) && currentPlayer == player2 && !takingScreenshot)
        {
            // Ensure the flash light is initially deactivated
            flashLight.gameObject.SetActive(false);

            takingScreenshot = true;
            challengeLocation = new Vector3(challengeLocationX, challengeLocationY, challengeLocationZ);
            // accuracyText.text = "Challenge location: (" + challengeLocation.x + ", " + challengeLocation.y + ", " + challengeLocation.z + ")";
            // accuracyText.gameObject.SetActive(true);

            StartCoroutine(CaptureSceneWithFlash());
        }
    }

    void SwitchPlayers()
    {
        takingScreenshot = false; // Ensure takingScreenshot flag is reset before switching players

        flashLight.gameObject.SetActive(false);

        currentPlayer.gameObject.SetActive(false);
        otherPlayer.gameObject.SetActive(true);

        Transform temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;
    }

    void SwitchBackToOriginalPlayer()
    {
        takingScreenshot = false; // Ensure takingScreenshot flag is reset before switching players

        flashLight.gameObject.SetActive(false);

        currentPlayer.gameObject.SetActive(false);
        otherPlayer.gameObject.SetActive(true);

        Transform temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;
    }

    void DeactivateCamera()
    {
        // Ensure the flash light is initially deactivated
        // flashLight.gameObject.SetActive(false);

        cameraObject.SetActive(false);
    }

    void ActivateCamera()
    {
        // Ensure the flash light is initially deactivated
        // flashLight.gameObject.SetActive(false);

        cameraObject.SetActive(true);
    }

    void HandleObjectsActivation()
    {
        // Ensure the flash light is initially deactivated
        // flashLight.gameObject.SetActive(false);

        if (currentPlayer == player1)
        {
            DeactivateObjects(player1ObjectsToDeactivate);
            ActivateObjects(player1ObjectsToActivate);
        }
        else if (currentPlayer == player2)
        {
            DeactivateObjects(player2ObjectsToDeactivate);
            ActivateObjects(player2ObjectsToActivate);
        }
    }

    void DeactivateObjects(GameObject[] objects)
    {
        // Ensure the flash light is initially deactivated
        // flashLight.gameObject.SetActive(false);

        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    void ActivateObjects(GameObject[] objects)
    {
        // Ensure the flash light is initially deactivated
        // flashLight.gameObject.SetActive(false);

        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

    IEnumerator CaptureSceneWithFlash()
    {
        // takingScreenshot = true; // Set takingScreenshot flag to true when capturing scene

        // Activate the light for the flash effect
        flashLight.gameObject.SetActive(true);
        flashLight.intensity = 3f;

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Deactivate the light
        flashLight.gameObject.SetActive(false);

        // Capture the screenshot
        CaptureScene();

        takingScreenshot = false; // Reset takingScreenshot flag after capturing scene
    }

    // void CaptureScene()
    // {
    //     // Create a RenderTexture with the same dimensions as the camera's viewport
    //     RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);

    //     // Set the camera to render to the RenderTexture
    //     Camera.main.targetTexture = renderTexture;
    //     Camera.main.Render();

    //     // Create a Texture2D to hold the screenshot
    //     Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

    //     // Read the pixels from the RenderTexture and apply them to the Texture2D
    //     RenderTexture.active = renderTexture;
    //     screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    //     screenshot.Apply();

    //     // Reset the camera's target texture
    //     Camera.main.targetTexture = null;
    //     RenderTexture.active = null;

    //     // Encode the Texture2D as a PNG and save it to disk
    //     byte[] bytes = screenshot.EncodeToPNG();
    //     string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
    //     string fileName = "Screenshot_" + timestamp + ".png";
    //     System.IO.File.WriteAllBytes(fileName, bytes);

    //     Debug.Log("Screenshot captured and saved as: " + fileName);

    //     // Display the captured photo for 2 seconds
    //     StartCoroutine(DisplayPhotoForDuration(screenshot, 2f));
    // }
    void CaptureScene()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        Camera.main.targetTexture = renderTexture;
        Camera.main.Render();

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        Camera.main.targetTexture = null;
        RenderTexture.active = null;

        byte[] bytes = screenshot.EncodeToPNG();
        string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        string fileName = "Screenshot_" + timestamp + ".png";
        System.IO.File.WriteAllBytes(fileName, bytes);

        Debug.Log("Screenshot captured and saved as: " + fileName);

        // Calculate the distance between the challenge location and player2
        float distanceToChallenge = Vector3.Distance(challengeLocation, player2.position);
        Debug.Log("Distance to Challenge: " + distanceToChallenge);

        // Define the maximum allowable distance for passing the challenge
        float maxAllowableDistance = 10f; // Adjust this value as needed
        Debug.Log("Max Allowable Distance: " + maxAllowableDistance);

        // Calculate the accuracy percentage based on the distance to challenge location
        float accuracyPercentage = 100f * (1f - (distanceToChallenge / maxAllowableDistance));
        accuracyPercentage = Mathf.Clamp(accuracyPercentage, 0f, 100f); // Ensure the percentage is between 0 and 100
        Debug.Log("Accuracy Percentage: " + accuracyPercentage);

        StartCoroutine(DisplayPhotoForDuration(screenshot, 2f));

        // Check if the accuracy percentage is greater than or equal to 90
        if (accuracyPercentage >= 90f && !challengePassed)
        {
            IncrementChallenge();
            challengePassed = true;

            Debug.Log("Challenge Passed!");
            passchallenge.gameObject.SetActive(true);
            StartCoroutine(DisplayPass(3f));
        }
        else
        {
            Debug.Log("Challenge Failed!");
        }
    }




    IEnumerator DisplayPhotoForDuration(Texture2D photo, float duration){
        // Set the photo texture to the RawImage component
        photoDisplayImage.texture = photo;

        // Display the RawImage component
        photoDisplayImage.gameObject.SetActive(true);

        // Wait for the specified display duration
        yield return new WaitForSeconds(duration);

        // Hide the RawImage component after displaying for the specified duration
        photoDisplayImage.gameObject.SetActive(false);
    }
    IEnumerator DisplayUITextsInOrder()
    {
        foreach (Text text in player2UITexts)
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(displayDuration);
            text.gameObject.SetActive(false);
        }
    }
    IEnumerator DisplayPass(float delay)
    {
        yield return new WaitForSeconds(delay);
        passchallenge.gameObject.SetActive(false);
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
}
