using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject scoreScreen;
    public GameObject ticTacToeBoard;

    public TextMeshProUGUI WinnerText;

    public GameObject winBarMlmr;
    public GameObject winBarUmlm;
    public GameObject winBarUllr;
    public GameObject winBarLlur;
    public GameObject winBarUlur;
    public GameObject winBarUlll;
    public GameObject winBarUrlr;
    public GameObject winBarLllr;

    private int _currentPLayer = 1;
    private bool _gameOver;

    private Dictionary<string, Square> _playBoard;
    private bool _runAI;
    private bool _aiRunning;
    private TextMeshProUGUI _squareTexts;

    private int _winner;

    public GameController(GameObject winBarUmlm)
    {
        this.winBarUmlm = winBarUmlm;
    }

    private void Start()
    {
        _playBoard = new Dictionary<string, Square>();
        _aiRunning = false;
        SetForStartGame();
    }

    private void Update()
    {
        if (_runAI && !_gameOver && !_aiRunning)
        {
            if (_currentPLayer == 0)
            {
                _aiRunning = true;
                int move = AIGetMove();
                SetMove(_playBoard.ElementAt(move).Key);
                _aiRunning = false;
            }
        }
    }

    public void SetForStartGame()
    {
        _gameOver = false;
        _currentPLayer = 1;
        _runAI = false;
        _aiRunning = false;
        _winner = 0;

        ticTacToeBoard.SetActive(false);
        scoreScreen.SetActive(false);
        startScreen.SetActive(true);
        winBarMlmr.SetActive(false);
        winBarUmlm.SetActive(false);
        winBarUllr.SetActive(false);
        winBarLlur.SetActive(false);
        winBarUlur.SetActive(false);
        winBarUlll.SetActive(false);
        winBarUrlr.SetActive(false);
        winBarLllr.SetActive(false);

        foreach (var entry in _playBoard.ToList())
        {
            Square square = entry.Value;
            square.TextField.text = string.Empty;
            square.PlayerOwned = " ";
            square.SquareButton.interactable = true;
            _playBoard[entry.Key] = square;
        }
    }

    public void RegisterSquareOnStartup(Button button, TextMeshProUGUI textField)
    {
        if (!_playBoard.ContainsKey(button.name))
        {
            Square newSquare = new(button, textField);
            _playBoard.Add(button.name, newSquare);
        }
    }

    private void StartGame(int numplayers)
    {
        _runAI = numplayers == 1;

        startScreen.SetActive(false);
        scoreScreen.SetActive(false);
        ticTacToeBoard.SetActive(true);
    }

    public void OnNumPLayersSelected(int numPlayers)
    {
        StartGame(numPlayers);
    }

    public void OnSquareSelected(Button button)
    {
        SetMove(button.name);
    }

    private void SetMove(string key)
    {
        Square square = _playBoard[key];
        if (_currentPLayer == 1)
        {
            square.TextField.text = "X";
            square.PlayerOwned = "X";
        }
        else
        {
            square.TextField.text = "O";
            square.PlayerOwned = "O";
        }

        _playBoard[key] = square;
        _currentPLayer ^= 1;
        square.SquareButton.interactable = false;

        CheckForWin();
    }

    private int AIGetMove()
    {
        char[,] board = new char[8, 8];
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                board[x, y] = char.Parse(_playBoard.ElementAt(y * 3 + x).Value.PlayerOwned);
            }
        }

        int bestMove = ComputeMove.FindBestMove(board, 'O');
        return bestMove;
    }

    private void CheckForWin()
    {
        // 8 checks for 8 win possibilities  multiple win types are possible

        string playerOwned = _playBoard["MM"].PlayerOwned;
        if (!playerOwned.Equals(" "))
        {
            // ML MM MR
            if (_playBoard["ML"].PlayerOwned.Equals(playerOwned) &&
                _playBoard["MR"].PlayerOwned.Equals(playerOwned))
            {
                _gameOver = true;
                winBarMlmr.SetActive(true);
            }

            // UM MM LM
            if (_playBoard["UM"].PlayerOwned.Equals(playerOwned) &&
                _playBoard["LM"].PlayerOwned.Equals(playerOwned))
            {
                _gameOver = true;
                winBarUmlm.SetActive(true);
            }

            // UL MM LR
            if (_playBoard["UL"].PlayerOwned.Equals(playerOwned) &&
                _playBoard["LR"].PlayerOwned.Equals(playerOwned))
            {
                _gameOver = true;
                winBarUllr.SetActive(true);
            }

            // LL MM UR
            if (_playBoard["LL"].PlayerOwned.Equals(playerOwned) &&
                _playBoard["UR"].PlayerOwned.Equals(playerOwned))
            {
                _gameOver = true;
                winBarLlur.SetActive(true);
            }
        }

        playerOwned = _playBoard["UL"].PlayerOwned;
        if (!playerOwned.Equals(" "))
        {
            //UL UM UR
            if (_playBoard["UM"].PlayerOwned.Equals(playerOwned) &&
                _playBoard["UR"].PlayerOwned.Equals(playerOwned))
            {
                _gameOver = true;
                winBarUlur.SetActive(true);
            }

            //UL ML LL
            if (_playBoard["ML"].PlayerOwned.Equals(playerOwned) &&
                _playBoard["LL"].PlayerOwned.Equals(playerOwned))
            {
                _gameOver = true;
                winBarUlll.SetActive(true);
            }
        }

        playerOwned = _playBoard["LR"].PlayerOwned;
        if (!playerOwned.Equals(" "))
        {
            // LL LM LR
            if (_playBoard["LL"].PlayerOwned.Equals(playerOwned) &&
                _playBoard["LM"].PlayerOwned.Equals(playerOwned))
            {
                _gameOver = true;
                winBarLllr.SetActive(true);
            }

            // UR MR LR
            if (_playBoard["UR"].PlayerOwned.Equals(playerOwned) &&
                _playBoard["MR"].PlayerOwned.Equals(playerOwned))
            {
                _gameOver = true;
                winBarUrlr.SetActive(true);
            }
        }

        if (!_gameOver)
        {
            bool filled = true;
            foreach (var entry in _playBoard)
            {
                if (entry.Value.PlayerOwned == " ")
                {
                    filled = false;
                    break;
                }
            }

            if (filled)
            {
                _gameOver = true;
                _winner = 0;
            }
        }
        else
        {
            _winner = _currentPLayer == 1 ? 2 : 1;
        }

        if (_gameOver)
        {
            scoreScreen.SetActive(true);
            if (_winner > 0)
            {
                WinnerText.text = "Player " + (_winner == 1 ? "One" : "Two");
            }
            else
            {
                WinnerText.text = "Draw Game";
            }
        }
    }

    private struct Square
    {
        public readonly Button SquareButton;
        public readonly TextMeshProUGUI TextField;
        public string PlayerOwned;

        public Square(Button button, TextMeshProUGUI textMeshProUGUI)
        {
            SquareButton = button;
            TextField = textMeshProUGUI;
            PlayerOwned = " ";
        }
    }
}
