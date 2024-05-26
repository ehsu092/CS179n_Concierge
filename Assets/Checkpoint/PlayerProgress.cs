using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public bool[] challengesCompleted; // Array to keep track of challenges

    private void Start()
    {
        // Initialize the array with the number of challenges
        challengesCompleted = new bool[3]; // Assuming 3 challenges
    }

    public void CompleteChallenge(int challengeIndex)
    {
        if (challengeIndex >= 0 && challengeIndex < challengesCompleted.Length)
        {
            challengesCompleted[challengeIndex] = true;
        }
    }

    public bool AllChallengesCompleted()
    {
        foreach (bool challenge in challengesCompleted)
        {
            if (!challenge)
            {
                return false;
            }
        }
        return true;
    }
}
