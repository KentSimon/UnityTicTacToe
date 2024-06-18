using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject ScoreScreen;
    public GameObject TicTacToeBoard;

    public TextMeshProUGUI WinnerText;

    public GameObject WinBarMLMR;
    public GameObject WinBarUMLM;
    public GameObject WinBarULLR;
    public GameObject WinBarLLUR;
    public GameObject WinBarULUR;
    public GameObject WinBarULLL;
    public GameObject WinBarURLR;
    public GameObject WinBarLLLR;

    private int currentPLayer = 1;
    private bool gameOver;

    private Dictionary<string, Square> PlayBoard;
    private bool runAI;
    private TextMeshProUGUI SquareTexts;

    private int winner;

    private void Start()
    {
        PlayBoard = new Dictionary<string, Square>();
        SetForStartGame();
    }

    private void Update()
    {
        bool aiRunning = false;
        if (runAI && !gameOver && !aiRunning)
            if (currentPLayer == 0)
            {
                aiRunning = true;
                int move = AIGetMove();
                SetMove(PlayBoard.ElementAt(move).Key);
            }
    }

    public void SetForStartGame()
    {
        gameOver = false;
        currentPLayer = 1;
        runAI = false;
        winner = 0;

        TicTacToeBoard.SetActive(false);
        ScoreScreen.SetActive(false);
        StartScreen.SetActive(true);
        WinBarMLMR.SetActive(false);
        WinBarUMLM.SetActive(false);
        WinBarULLR.SetActive(false);
        WinBarLLUR.SetActive(false);
        WinBarULUR.SetActive(false);
        WinBarULLL.SetActive(false);
        WinBarURLR.SetActive(false);
        WinBarLLLR.SetActive(false);

        foreach (var entry in PlayBoard.ToList())
        {
            Square square = entry.Value;
            square.textField.text = string.Empty;
            square.playerOwned = " ";
            square.button.interactable = true;
            PlayBoard[entry.Key] = square;
        }
    }

    public void RegisterSquareOnStartup(Button button, TextMeshProUGUI textField)
    {
        if (!PlayBoard.ContainsKey(button.name))
        {
            Square newSquare = new(button, textField);
            PlayBoard.Add(button.name, newSquare);
        }
    }

    public void StartGame(int numplayers)
    {
        if (numplayers == 1)
            runAI = true;
        else
            runAI = false;

        StartScreen.SetActive(false);
        ScoreScreen.SetActive(false);
        TicTacToeBoard.SetActive(true);
    }

    public void OnNumPLayersSelected(int numPlayers)
    {
        StartGame(numPlayers);
    }

    public void OnSquareSelected(Button button)
    {
        SetMove(button.name);
    }

    public void SetMove(string key)
    {
        Square square = PlayBoard[key];
        if (currentPLayer == 1)
        {
            square.textField.text = "X";
            square.playerOwned = "X";
        }
        else
        {
            square.textField.text = "O";
            square.playerOwned = "O";
        }

        PlayBoard[key] = square;
        currentPLayer ^= 1;
        square.button.interactable = false;

        CheckForWin();
    }

    private int AIGetMove()
    {
        char[,] board = new char[8, 8];
        for (int y = 0; y < 3; y++)
        for (int x = 0; x < 3; x++)
            board[x, y] = char.Parse(PlayBoard.ElementAt(y * 3 + x).Value.playerOwned);

        int bestMove = ComputeMove.FindBestMove(board, 'O');
        return bestMove;
    }

    private void CheckForWin()
    {
        // 8 checks for 8 win possibilities  multiple win types are possible

        string playerOwned = PlayBoard["MM"].playerOwned;
        if (!playerOwned.Equals(" "))
        {
            // ML MM MR
            if (PlayBoard["ML"].playerOwned.Equals(playerOwned) &&
                PlayBoard["MR"].playerOwned.Equals(playerOwned))
            {
                gameOver = true;
                WinBarMLMR.SetActive(true);
            }

            // UM MM LM
            if (PlayBoard["UM"].playerOwned.Equals(playerOwned) &&
                PlayBoard["LM"].playerOwned.Equals(playerOwned))
            {
                gameOver = true;
                WinBarUMLM.SetActive(true);
            }

            // UL MM LR
            if (PlayBoard["UL"].playerOwned.Equals(playerOwned) &&
                PlayBoard["LR"].playerOwned.Equals(playerOwned))
            {
                gameOver = true;
                WinBarULLR.SetActive(true);
            }

            // LL MM UR
            if (PlayBoard["LL"].playerOwned.Equals(playerOwned) &&
                PlayBoard["UR"].playerOwned.Equals(playerOwned))
            {
                gameOver = true;
                WinBarLLUR.SetActive(true);
            }
        }


        playerOwned = PlayBoard["UL"].playerOwned;
        if (!playerOwned.Equals(" "))
        {
            //UL UM UR
            if (PlayBoard["UM"].playerOwned.Equals(playerOwned) &&
                PlayBoard["UR"].playerOwned.Equals(playerOwned))
            {
                gameOver = true;
                WinBarULUR.SetActive(true);
            }

            //UL ML LL
            if (PlayBoard["ML"].playerOwned.Equals(playerOwned) &&
                PlayBoard["LL"].playerOwned.Equals(playerOwned))
            {
                gameOver = true;
                WinBarULLL.SetActive(true);
            }
        }


        playerOwned = PlayBoard["LR"].playerOwned;
        if (!playerOwned.Equals(" "))
        {
            // LL LM LR
            if (PlayBoard["LL"].playerOwned.Equals(playerOwned) &&
                PlayBoard["LM"].playerOwned.Equals(playerOwned))
            {
                gameOver = true;
                WinBarLLLR.SetActive(true);
            }

            // UR MR LR
            if (PlayBoard["UR"].playerOwned.Equals(playerOwned) &&
                PlayBoard["MR"].playerOwned.Equals(playerOwned))
            {
                gameOver = true;
                WinBarURLR.SetActive(true);
            }
        }

        if (!gameOver)
        {
            bool filled = true;
            foreach (var entry in PlayBoard)
            {
                if (entry.Value.playerOwned == " ")
                {
                    filled = false;
                    break;
                }
            }

            if (filled)
            {
                gameOver = true;
                winner = 0;
            }
        }
        else
        {
            winner = currentPLayer == 1 ? 2 : 1;
        }


        if (gameOver)
        {
            ScoreScreen.SetActive(true);
            if (winner > 0)
            {
                WinnerText.text = "Player " + (winner == 1 ? "One" : "Two");
            }
            else
            {
                WinnerText.text = "Draw Game";
            }

        }
    }

    private struct Square
    {
        public readonly Button button;
        public readonly TextMeshProUGUI textField;
        public string playerOwned;

        public Square(Button button1, TextMeshProUGUI textMeshProUGUI)
        {
            button = button1;
            textField = textMeshProUGUI;
            playerOwned = " ";
        }
    }
}
