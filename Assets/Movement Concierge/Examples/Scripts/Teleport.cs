using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Teleport : MonoBehaviour
{
    public GameObject thePlayer;

    // Target position coordinates
    public float targetX;
    public float targetY;
    public float targetZ;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == thePlayer)
        {
            // Find the pickup_flashlight script
            pickup_flashlight pickupScript = FindObjectOfType<pickup_flashlight>();

            // Check if pickupScript is found
            if (pickupScript != null)
            {
                // Output the challenge count
                Debug.Log("Challenge count: " + pickupScript.challenge);
                
                // pickupScript.challenge = 7;
                // Check if the challenge count is less than 7
                if (pickupScript.challenge < 7)
                {
                    // Calculate the teleport target position
                    Vector3 teleportTargetPosition = new Vector3(targetX, targetY, targetZ);

                    // Teleport the player to the target position
                    thePlayer.transform.position = teleportTargetPosition;

                    // Calculate the opposite direction relative to the player's current facing direction
                    Vector3 currentForward = thePlayer.transform.forward;
                    Vector3 oppositeDirection = -currentForward;

                    // Make the player face the opposite direction
                    thePlayer.transform.rotation = Quaternion.LookRotation(oppositeDirection);

                    // Log teleportation
                    Debug.Log("Player teleported to: " + teleportTargetPosition);
                }
                else
                {
                    // Log that teleportation is not executed
                    Debug.Log("Challenge count is 7 or greater. Teleportation not executed.");
                }
            }
            else
            {
                Debug.LogError("pickup_flashlight script not found.");
            }
        }
    }
}
