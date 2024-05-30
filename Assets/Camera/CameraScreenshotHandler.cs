using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraScreenshotHandler : MonoBehaviour
{
    public Camera mainCamera; // Drag the main camera here in the Unity Editor
    public Camera cameraToCapture; // Drag the capture camera here in the Unity Editor
    public RenderTexture renderTexture;
    public string screenshotFilename = "camera_screenshot.png";
    public Transform playerTransform;
    public float detectionDistance = 5.0f; // Distance to trigger UI display
    public Text distanceText;
    public Text instructionText;

    private bool isDisplayingInstruction = false;

    void Start()
    {
        // Ensure the main camera and the camera to capture have their target textures set
        mainCamera.targetTexture = renderTexture;
        cameraToCapture.targetTexture = renderTexture;

        // Disable the capture camera at the start
        cameraToCapture.enabled = false;

        // Hide UI text elements at start
        distanceText.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, cameraToCapture.transform.position);
        if (distance <= detectionDistance && !isDisplayingInstruction)
        {
            StartCoroutine(DisplayDistanceText());
            StartCoroutine(DisplayInstructionText());
        }

        if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to switch to the capture camera and take a screenshot
        {
            SwitchToCaptureCamera();
            TakeCameraScreenshot();
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Press 'Q' to switch back to the main camera
        {
            SwitchToMainCamera();
        }
    }

    void SwitchToCaptureCamera()
    {
        // Disable the main camera
        mainCamera.enabled = false;

        // Enable the capture camera
        cameraToCapture.enabled = true;

        Debug.Log("Switched to capture camera");
    }

    void SwitchToMainCamera()
    {
        // Disable the capture camera
        cameraToCapture.enabled = false;

        // Enable the main camera
        mainCamera.enabled = true;

        Debug.Log("Switched to main camera");
    }

    void TakeCameraScreenshot()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        cameraToCapture.Render();

        Texture2D image = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        image.Apply();

        RenderTexture.active = currentRT;

        byte[] bytes = image.EncodeToPNG();
        string path = Application.dataPath + "/" + screenshotFilename;
        System.IO.File.WriteAllBytes(path, bytes);

        Debug.Log("Camera screenshot taken and saved to " + path);
    }

    IEnumerator DisplayDistanceText()
    {
        distanceText.gameObject.SetActive(true);
        distanceText.text = "You are close to the camera!";
        yield return new WaitForSeconds(3);
        distanceText.gameObject.SetActive(false);
    }

    IEnumerator DisplayInstructionText()
    {
        isDisplayingInstruction = true;
        instructionText.gameObject.SetActive(true);
        instructionText.text = "Press 'E' to capture or 'Q' to quit";
        yield return new WaitForSeconds(3);
        instructionText.gameObject.SetActive(false);
        isDisplayingInstruction = false;
    }
}
