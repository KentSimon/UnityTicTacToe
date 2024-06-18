public static class ComputeMove
{
    public static int FindBestMove(char[,] board, char player)
    {
        int bestScore = int.MinValue;
        int moveI = -1;
        int moveJ = -1;

        for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
            if (board[i, j] == ' ')
            {
                board[i, j] = player;
                int score = Minimax(board, 'O');
                board[i, j] = ' ';

                if (score > bestScore)
                {
                    bestScore = score;
                    moveI = i;
                    moveJ = j;
                }
            }

        return moveI + moveJ * 3;
    }

    private static int Minimax(char[,] board, char forWho)
    {
        int score = CheckWhoWins(board, forWho);
        if (score != 0) return score;

        int bestScore = forWho == 'O' ? int.MinValue : int.MaxValue;


        for (int i = 0; i < 3; i++)

        for (int j = 0; j < 3; j++)
            if (board[i, j] == ' ')
            {
                board[i, j] = forWho;
                int currentScore = Minimax(board, forWho == 'O' ? 'X' : 'O');
                board[i, j] = ' ';

                bestScore = CalcBest(bestScore, currentScore);
            }

        return bestScore;

        int CalcBest(int x, int y)
        {
            return (forWho == 'O' ? x > y : y > x) ? x : y;
        }
    }

    private static int CheckWhoWins(char[,] board, char forWho)
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
            int score = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        score++;
                    }
                }
            }

            return score;
        }

        return 0;
    }
}
