using UnityEngine;

public class TicTacToeCell : MonoBehaviour
{
    public int cellIndex;
    public Texture2D xTexture; // Texture for X
    public Texture2D oTexture; // Texture for O
    private MeshRenderer meshRenderer; // Renderer for the mesh
    private bool isClickable = true; // Track if the cell is clickable

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Unlit/Transparent"));
        meshRenderer.material.color = Color.white; // Set the base color to white
    }

    public void SetSymbol(string symbol)
    {
        // Set the texture based on the player's move
        if (symbol == "X")
        {
            meshRenderer.material.mainTexture = xTexture;
        }
        else if (symbol == "O")
        {
            meshRenderer.material.mainTexture = oTexture;
        }

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
