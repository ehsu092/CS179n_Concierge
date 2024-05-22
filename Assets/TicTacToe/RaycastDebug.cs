using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDebug : MonoBehaviour
{
    public float interactionRange = 5.0f;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            Debug.LogError("No camera found on the player.");
        }
        else
        {
            Debug.Log("Camera found: " + playerCamera.name);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                Debug.Log("Hit: " + hit.transform.name);
            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }
}
