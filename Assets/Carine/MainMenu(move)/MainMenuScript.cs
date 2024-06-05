//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class MenuScript : MonoBehaviour
//{
//    public GameObject menuCanvas; // Reference to the menu canvas to disable it when playing the game

//    public void PlayGame()
//    {
//        // Hide the menu canvas
//        if (menuCanvas != null)
//        {
//            menuCanvas.SetActive(false);
//        }

//        // Load the next scene
//        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
//        SceneManager.LoadScene(nextSceneIndex);
//    }

//    public void QuitGame()
//    {
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#else
//        Application.Quit();
//#endif
//    }
//}

using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject menuCanvas; // Reference to the menu canvas
    public GameObject gameplayObjects; // Reference to the gameplay objects

    public void PlayGame()
    {
        // Hide the menu canvas
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(false);
        }

        // Show the gameplay objects
        if (gameplayObjects != null)
        {
            gameplayObjects.SetActive(true);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}