using System;

public class ComputeMove
{
    public static int FindBestMove(char[,] board, char player)
    {
        int bestScore = int.MinValue;
        int moveI = -1;
        int moveJ = -1;

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    board[i, j] = 'O';
                    var score = Minimax(board, 'O');
                    board[i, j] = ' ';

                    if (score > bestScore)
                    {
                        bestScore = score;
                        moveI = i;
                        moveJ = j;
                    }
                }
            }
        }

        return moveI + moveJ*3;
    }

    static int Minimax(char[,] board, char forWho)
    {
        var score = CheckWhoWins(board, forWho);
        if (score != 0)
        {
            return score;
        }

        var bestScore = forWho == 'O' ? int.MinValue : int.MaxValue;

        int CalcBest(int x, int y) => (forWho == 'O' ? x > y : y > x) ? x : y;

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    board[i, j] = forWho;
                    var currentScore = Minimax(board, forWho == 'O' ? 'X' : 'O');
                    board[i, j] = ' ';

                    bestScore = CalcBest(bestScore, currentScore);
                }
            }
        }

        return bestScore;
    }

    static int CheckWhoWins(char[,] board, char forWho)
    {
        if ((board[0, 0] == forWho && board[0, 1] == forWho && board[0, 2] == forWho)
            || (board[1, 0] == forWho && board[1, 1] == forWho && board[1, 2] == forWho)
            || (board[2, 0] == forWho && board[2, 1] == forWho && board[2, 2] == forWho)
            || (board[0, 0] == forWho && board[1, 0] == forWho && board[2, 0] == forWho)
            || (board[0, 1] == forWho && board[1, 1] == forWho && board[2, 1] == forWho)
            || (board[0, 2] == forWho && board[1, 2] == forWho && board[2, 2] == forWho)
            || (board[0, 0] == forWho && board[1, 1] == forWho && board[2, 2] == forWho)
            || (board[0, 2] == forWho && board[1, 1] == forWho && board[2, 0] == forWho))
        {
            var score = 1;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        score++;
                    }
                }
            }

            return score;
        }
        else
            return 0;
    }
}
