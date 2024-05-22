using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeInteraction : MonoBehaviour
{
    public float interactionRange = 5.0f;
    public LayerMask interactableLayer;

    private Camera playerCamera;
    private TicTacToeGame game;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        game = FindObjectOfType<TicTacToeGame>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                Debug.Log("Hit: " + hit.transform.name);  // Add this line to debug
                TicTacToeCell cell = hit.transform.GetComponent<TicTacToeCell>();
                if (cell != null)
                {
                    game.OnCellClicked(cell);
                }
                else
                {
                    Debug.Log("No TicTacToeCell component found on the hit object.");
                }
            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }
}
