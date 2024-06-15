using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    struct Square
    {
        public Button button;
        public TextMeshProUGUI textField;
        public string playerOwned;

        public Square(Button button1, TextMeshProUGUI textMeshProUGUI)
        {
            this.button = button1;
            this.textField = textMeshProUGUI;
            playerOwned = "";
        }
    }

    public GameObject StartScreen;
    public GameObject ScoreScreen;
    public GameObject TicTacToeBoard;

    private Dictionary<string, Square> PlayBoard;
    private TextMeshProUGUI SquareTexts;
    private bool gameOver = false;
    private bool runAI = false;

    public GameObject WinBarMLMR;
    public GameObject WinBarUMLM;
    public GameObject WinBarULLR;
    public GameObject WinBarLLUR;
    public GameObject WinBarULUR;
    public GameObject WinBarULLL;
    public GameObject WinBarURLR;
    public GameObject WinBarLLLR;

    private int currentPLayer = 1;


    void Start()
    {
        PlayBoard = new Dictionary<string, Square>();
        SetForStartGame();
    }

    public void SetForStartGame()
    {
                gameOver = false;
        currentPLayer = 1;
        runAI = false;

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
            square.textField.text = String.Empty;
            square.playerOwned = String.Empty;
            square.button.interactable = true;
            PlayBoard[entry.Key] = square;
        }

    }

    public void RegisterSquareOnStartup(Button button, TextMeshProUGUI textField)
    {
        Debug.LogFormat("Registering Square {0}",button.name);
        if (!PlayBoard.ContainsKey(button.name))
        {
            Square newSquare = new Square(button, textField);
            PlayBoard.Add(button.name, newSquare);
        }
    }

    public void StartGame(int numplayers)
    {
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
        Square square = PlayBoard[button.name];
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

        PlayBoard[button.name] = square;
        currentPLayer ^= 1;
        square.button.interactable = false;

        CheckForWin();
    }

    private void CheckForWin()
    {
        // 8 checks for 8 win possibilities  multiple win types are possible

        string playerOwned = PlayBoard["MM"].playerOwned;
        if (!string.IsNullOrEmpty(playerOwned))
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
        if (!string.IsNullOrEmpty(playerOwned))
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
        if (!string.IsNullOrEmpty(playerOwned))
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

        if (gameOver)
        {
            ScoreScreen.SetActive(true);
        }
    }
}
