using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeCheckpoint : MonoBehaviour
{
    public int challengeIndex;

    private void OnTriggerEnter(Collider other)
    {
        PlayerProgress playerProgress = other.GetComponent<PlayerProgress>();
        if (playerProgress != null)
        {
            playerProgress.CompleteChallenge(challengeIndex);
        }
    }
}
