using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRepeater : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public float repeatInterval = 10.0f; // Time interval in seconds

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource != null)
        {
            StartCoroutine(RepeatSound());
        }
        else
        {
            Debug.LogError("AudioSource component not found!");
        }
    }

    IEnumerator RepeatSound()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(repeatInterval);
        }
    }
}

