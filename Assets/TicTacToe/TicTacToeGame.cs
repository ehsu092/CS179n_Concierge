using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeGame : MonoBehaviour
{
    public static TicTacToeGame Instance { get; private set; }

    public TicTacToeCell[] cells;
    private string currentPlayer;
    private int[] gameState; // 0: Empty, 1: X, -1: O

    public GameObject winObjectPrefab; // Prefab of the object to spawn when X wins
    private GameObject winObjectInstance; // Instance of the spawned object

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentPlayer = "X";
        gameState = new int[9]; // Initialize game state

        // Automatically find and assign cells by tag
        GameObject[] cellObjects = GameObject.FindGameObjectsWithTag("Cell");
        cells = new TicTacToeCell[cellObjects.Length];
        for (int i = 0; i < cellObjects.Length; i++)
        {
            cells[i] = cellObjects[i].GetComponent<TicTacToeCell>();
            cells[i].cellIndex = i;
        }
    }

    public void OnCellClicked(TicTacToeCell cell)
    {
        int index = cell.cellIndex;
        if (gameState[index] == 0) // Check if the cell is empty
        {
            gameState[index] = (currentPlayer == "X") ? 1 : -1;
            cell.SetSymbol(currentPlayer);
            if (CheckWin())
            {
                Debug.Log(currentPlayer + " Wins!");
                if (currentPlayer == "X")
                {
                    SpawnWinObject();
                }
                ResetGame();
            }
            else if (IsBoardFull())
            {
                Debug.Log("Draw!");
                ResetGame();
            }
            else
            {
                currentPlayer = (currentPlayer == "X") ? "O" : "X";
                if (currentPlayer == "O")
                {
                    // Call AI to make a move
                    MakeAIMove();
                }
            }
        }
        Debug.Log("Cell clicked: " + index);
    }

    void SpawnWinObject()
    {
        // Spawn the win object next to the Main Camera's position
        if (winObjectPrefab != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 spawnOffset = Camera.main.transform.right * 1f - Camera.main.transform.up * 1f; // Spawn 1 unit to the right and 1 unit down
            Vector3 spawnPosition = cameraPosition + spawnOffset;
            winObjectInstance = Instantiate(winObjectPrefab, spawnPosition, Quaternion.identity);
        }

        // Open the door if X wins
        GameObject doorObject = GameObject.FindWithTag("LowPolyDoor2"); // Assuming the door GameObject has a "LowPolyDoor2" tag
        if (doorObject != null)
        {
            DoorController doorController = doorObject.GetComponent<DoorController>();
            if (doorController != null)
            {
                doorController.opening = true;
            }
        }
    }

    void MakeAIMove()
    {
        int bestMove = -1;
        int bestScore = int.MinValue;

        List<int> availableMoves = GetAvailableMoves();

        foreach (int move in availableMoves)
        {
            if (gameState[move] == 0 && cells[move].IsClickable()) // Check if the cell is empty and clickable
            {
                gameState[move] = -1; // Simulate move
                int score = Minimax(gameState, 0, false);
                gameState[move] = 0; // Undo move

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }
        }

        if (bestMove != -1)
        {
            gameState[bestMove] = -1;
            cells[bestMove].SetSymbol(currentPlayer);

            // Disable the cell after AI makes its move
            cells[bestMove].DisableCell();

            if (CheckWin())
            {
                Debug.Log(currentPlayer + " Wins!");
                ResetGame();
            }
            else if (IsBoardFull())
            {
                Debug.Log("Draw!");
                ResetGame();
            }
            else
            {
                currentPlayer = "X";
            }
        }
    }

    int Minimax(int[] board, int depth, bool isMaximizing)
    {
        if (CheckWin() && currentPlayer == "O") return 10 - depth;
        else if (CheckWin() && currentPlayer == "X") return depth - 10;
        else if (IsBoardFull()) return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;

            List<int> availableMoves = GetAvailableMoves();

            foreach (int move in availableMoves)
            {
                board[move] = -1; // Simulate move
                int score = Minimax(board, depth + 1, false);
                board[move] = 0; // Undo move
                bestScore = Mathf.Max(bestScore, score);
            }

            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;

            List<int> availableMoves = GetAvailableMoves();

            foreach (int move in availableMoves)
            {
                board[move] = 1; // Simulate move
                int score = Minimax(board, depth + 1, true);
                board[move] = 0; // Undo move
                bestScore = Mathf.Min(bestScore, score);
            }

            return bestScore;
        }
    }

    List<int> GetAvailableMoves()
    {
        List<int> availableMoves = new List<int>();

        for (int i = 0; i < gameState.Length; i++)
        {
            if (gameState[i] == 0)
            {
                availableMoves.Add(i);
            }
        }

        return availableMoves;
    }

    bool CheckWin()
    {
        int[][] winPatterns = new int[][]
        {
            new int[] {0, 1, 2}, new int[] {3, 4, 5}, new int[] {6, 7, 8}, // Rows
            new int[] {0, 3, 6}, new int[] {1, 4, 7}, new int[] {2, 5, 8}, // Columns
            new int[] {0, 4, 8}, new int[] {2, 4, 6}                       // Diagonals
        };

        foreach (var pattern in winPatterns)
        {
            int sum = gameState[pattern[0]] + gameState[pattern[1]] + gameState[pattern[2]];
            if (sum == 3 || sum == -3)
            {
                return true;
            }
        }
        return false;
    }

    bool IsBoardFull()
    {
        foreach (int state in gameState)
        {
            if (state == 0) return false;
        }
        return true;
    }

    void ResetGame()
    {
        gameState = new int[9];
        foreach (TicTacToeCell cell in cells)
        {
            cell.SetSymbol("");
        }
        currentPlayer = "X";
    }
}
