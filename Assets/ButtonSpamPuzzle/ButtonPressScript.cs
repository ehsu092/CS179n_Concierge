using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class ButtonPressScript : MonoBehaviour
{
    public GameObject Computer;
    public float interactionDistance = 10;
    private GameObject player;

    private bool challengePassed = false;

    public float timeRemain = 15;
    public bool timerRun = false;
    public static double count = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player object by accessing the first person controller
        GameObject firstPersonController = GameObject.Find("Character");
        if (firstPersonController != null)
        {
            player = firstPersonController.transform.GetChild(0).gameObject;
            if (player == null)
            {
                Debug.LogError("Player not found. Make sure the player object is a child of the FPSController.");
            }
        }
        else
        {
            Debug.LogError("FPSController not found. Make sure the FPSController object is in the scene.");
        }

        timerRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerClose()) {
            Debug.Log("accessed");
            count++;
            Debug.Log(count);
        }

        if(timerRun && count < 7){
            if(timeRemain > 0){
                timeRemain -= Time.deltaTime;
                //Debug.Log(timeRemain);
            } else {
                //Debug.Log("DONE");
                timeRemain = 0;
                timerRun = false;
            }
        }
        else if (timerRun && count >= 7){
            //IncrementChallenge();
            challengePassed = true;
            //Debug.Log("FINISHED");
        }
        else {
            Debug.Log("FAILURE");
        }

    }

    /*
    private void IncrementChallenge(){
        // Find the pickup_flashlight script and call IncrementChallenge method
        pickup_flashlight pickupScript = FindObjectOfType<pickup_flashlight>;
        if (pickupScript!= null){
            pickupScript.IncrementChallenge();
        }
        else {
            Debug.LogError("pickup_flashlight script not found.");
        }
    } */

    private bool IsPlayerClose(){
        if (player != null)
        {
            // Calculate the distance between the player and the computer
            float distance = Vector3.Distance(player.transform.position, Computer.transform.position);

            // Debug distance for troubleshooting
            Debug.Log("Distance to computer: " + distance);

            // Return true if the distance is within the interaction distance threshold
            return distance <= interactionDistance;
        }
        else
        {
            return false;
        }
    }
}
