using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerProgress playerProgress = other.GetComponent<PlayerProgress>();
        if (playerProgress != null)
        {
            if (playerProgress.AllChallengesCompleted())
            {
                // Load next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                // Reset to first level
                SceneManager.LoadScene(0);
            }
        }
    }
}