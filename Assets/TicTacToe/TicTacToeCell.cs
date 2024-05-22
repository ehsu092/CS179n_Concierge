using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeCell : MonoBehaviour
{
    public int cellIndex;
    private Renderer renderer;
    private bool isClickable = true; // Track if the cell is clickable

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetSymbol(string symbol)
    {
        // Change color to indicate the player's move
        if (symbol == "X")
            renderer.material.color = Color.red;
        else if (symbol == "O")
            renderer.material.color = Color.blue;
        
        // Disable collider to make the cell unclickable after being clicked
        if (symbol != "")
        {
            isClickable = false;
            Collider collider = GetComponent<Collider>();
            if (collider != null)
                collider.enabled = false;
        }
    }

    // Check if the cell is clickable
    public bool IsClickable()
    {
        return isClickable;
    }

    // Method to manually disable the cell (e.g., when clicked by AI)
    public void DisableCell()
    {
        isClickable = false;
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;
    }

    // Method called when the cell is clicked
    void OnMouseDown()
    {
        if (isClickable)
        {
            TicTacToeGame.Instance.OnCellClicked(this);
        }
    }
}
