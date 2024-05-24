using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint_Click : MonoBehaviour
{
    public GameObject Paint;
    public float interactionDistance = 15;

    public static int paintCount = 0;
    public static bool puzzleStatus = false; 

    private GameObject player;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleStatus == false){
            if (Input.GetKeyDown(KeyCode.E) && IsPlayerClose())
            {
                if(Paint.activeSelf){
                    Paint.SetActive(false);
                    paintCount++;
                } 
                
                Debug.Log("paintCount = " + paintCount);
            }

            //currently not have room paint
            if(paintCount >= 4){
                puzzleStatus = true;
            }
        }
        

        if (puzzleStatus == true){
            //puzzle finish
        }
    }


    private bool IsPlayerClose(){
        if (player != null)
        {
            // Calculate the distance between the player and the painting
            float distance = Vector3.Distance(player.transform.position, Paint.transform.position);

            // Debug distance for troubleshooting
            Debug.Log("Distance to painting: " + distance);

            // Return true if the distance is within the interaction distance threshold
            return distance <= interactionDistance;
        }
        else
        {
            return false;
        }
    }
}