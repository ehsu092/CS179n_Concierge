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
            if (CheckWin(gameState))
            {
                Debug.Log(currentPlayer + " Wins!");
                if (currentPlayer == "X")
                {
                    SpawnWinObject();
                }
                ResetGame();
            }
            else if (IsBoardFull(gameState))
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
                int score = Minimax(gameState, 0, false, int.MinValue, int.MaxValue);
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
            cells[bestMove].SetSymbol("O");

            // Disable the cell after AI makes its move
            cells[bestMove].DisableCell();

            if (CheckWin(gameState))
            {
                Debug.Log("O Wins!");
                ResetGame();
            }
            else if (IsBoardFull(gameState))
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

    int Minimax(int[] board, int depth, bool isMaximizing, int alpha, int beta)
    {
        int result = EvaluateBoard(board);
        if (result != 0)
            return result;

        if (IsBoardFull(board))
            return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;

            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    board[i] = -1; // AI is O
                    int score = Minimax(board, depth + 1, false, alpha, beta);
                    board[i] = 0;
                    bestScore = Mathf.Max(score, bestScore);
                    alpha = Mathf.Max(alpha, score);
                    if (beta <= alpha)
                        break;
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;

            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    board[i] = 1; // Player is X
                    int score = Minimax(board, depth + 1, true, alpha, beta);
                    board[i] = 0;
                    bestScore = Mathf.Min(score, bestScore);
                    beta = Mathf.Min(beta, score);
                    if (beta <= alpha)
                        break;
                }
            }
            return bestScore;
        }
    }

    int EvaluateBoard(int[] board)
    {
        int[][] winPatterns = new int[][]
        {
            new int[] {0, 1, 2}, new int[] {3, 4, 5}, new int[] {6, 7, 8}, // Rows
            new int[] {0, 3, 6}, new int[] {1, 4, 7}, new int[] {2, 5, 8}, // Columns
            new int[] {0, 4, 8}, new int[] {2, 4, 6}                       // Diagonals
        };

        foreach (var pattern in winPatterns)
        {
            int sum = board[pattern[0]] + board[pattern[1]] + board[pattern[2]];
            if (sum == 3)
            {
                return 10; // X wins
            }
            else if (sum == -3)
            {
                return -10; // O wins
            }
        }
        return 0; // No one wins
    }

    bool IsBoardFull(int[] board)
    {
        foreach (int state in board)
        {
            if (state == 0) return false;
        }
        return true;
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

    void ResetGame()
    {
        gameState = new int[9];
        foreach (TicTacToeCell cell in cells)
        {
            cell.SetSymbol("");
            cell.DisableCell();
        }
        currentPlayer = "X";
    }

    bool CheckWin(int[] board)
    {
        int[][] winPatterns = new int[][]
        {
            new int[] {0, 1, 2}, new int[] {3, 4, 5}, new int[] {6, 7, 8}, // Rows
            new int[] {0, 3, 6}, new int[] {1, 4, 7}, new int[] {2, 5, 8}, // Columns
            new int[] {0, 4, 8}, new int[] {2, 4, 6}                       // Diagonals
        };

        foreach (var pattern in winPatterns)
        {
            int sum = board[pattern[0]] + board[pattern[1]] + board[pattern[2]];
            if (sum == 3 || sum == -3)
            {
                return true;
            }
        }
        return false;
    }
}
