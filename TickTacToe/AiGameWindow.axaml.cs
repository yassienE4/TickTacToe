using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace TickTacToe;

public partial class AiGameWindow : Window
{
    public AiGameWindow(int d)
    {
        diffuculty = d;
        InitializeComponent();
        buttonMatrix = new List<List<Button>>()
        {
            new List<Button>() {TL,TM, TR},
            new List<Button>() {ML,MM, MR},
            new List<Button>() {BL,BM, BR},
        };
    }
    
    public enum CellState
    {
        Empty = -1,
        PlayerX = 1,
        PlayerO = 0
    }
    
        private int turn = 0;
        private int diffuculty = 0;
        private int mcount = 0;

        private List<List<CellState>> gameMatrix = new List<List<CellState>>()
        {
            new List<CellState>() { CellState.Empty, CellState.Empty, CellState.Empty },
            new List<CellState>() { CellState.Empty, CellState.Empty, CellState.Empty },
            new List<CellState>() { CellState.Empty, CellState.Empty, CellState.Empty },
        };

        private List<List<Button>> buttonMatrix;

        private void win(string x)
        {
            if (x == "X")
            {
                Winner.Text = "Winner= X";
                PopupMessage.Text = "X won";
                WinnerPopup.IsOpen = true;
            }
            if(x == "O")
            {
                Winner.Text = "Winner= O";
                PopupMessage.Text = "O won";
                WinnerPopup.IsOpen = true;
            }
            if(x == "Draw")
            {
                Winner.Text = "DRAW!";
                PopupMessage.Text = "DRAW";
                WinnerPopup.IsOpen = true;
            }
        }

        private bool CheckWin(int row, int col, CellState player) // used by player - more efficient as it knows the last move
        {
            // check the row of the last move
            if (gameMatrix[row][0] == player && gameMatrix[row][1] == player && gameMatrix[row][2] == player)
                return true;

            // check the column of the last move
            if (gameMatrix[0][col] == player && gameMatrix[1][col] == player && gameMatrix[2][col] == player)
                return true;

            // check the diagonal if applicable
            if (row == col && gameMatrix[0][0] == player && gameMatrix[1][1] == player && gameMatrix[2][2] == player)
                return true;

            // check the other diagonal if applicable
            if (row + col == 2 && gameMatrix[0][2] == player && gameMatrix[1][1] == player && gameMatrix[2][0] == player)
                return true;

            return false;
        }
        
        private bool CheckWinALL(CellState player) // used by ai
        {
            // Rows
            for (int i = 0; i < 3; i++)
            {
                if (gameMatrix[i][0] == player && gameMatrix[i][1] == player && gameMatrix[i][2] == player)
                    return true;
            }
            // Columns
            for (int i = 0; i < 3; i++)
            {
                if (gameMatrix[0][i] == player && gameMatrix[1][i] == player && gameMatrix[2][i] == player)
                    return true;
            }
            // Diagonals
            if (gameMatrix[0][0] == player && gameMatrix[1][1] == player && gameMatrix[2][2] == player)
                return true;
            if (gameMatrix[0][2] == player && gameMatrix[1][1] == player && gameMatrix[2][0] == player)
                return true;

            return false;
        }
        
        private bool CheckDraw()
        {
            // Iterate through the game matrix
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // If any cell is empty, it's not a draw
                    if (gameMatrix[i][j] == CellState.Empty)
                    {
                        return false;
                    }
                }
            }

            // If no player has won and all cells are filled, it's a draw
            return true;
        }
        
        private void Turn_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null || button.IsEnabled == false) return;

            string name = button.Name; // Example: "TL"
            int row = name[0] == 'T' ? 0 : name[0] == 'M' ? 1 : 2;
            int col = name[1] == 'L' ? 0 : name[1] == 'M' ? 1 : 2;

            // Player X turn
            if (turn % 2 == 0)
            {
                button.Content = "X";
                gameMatrix[row][col] = CellState.PlayerX;
                button.IsEnabled = false;

                if (CheckWin(row, col, CellState.PlayerX))
                {
                    win("X");
                    return;
                }
                turn++;
                aiTurn();
            }
        }

        private void Restart_OnClick(object? sender, RoutedEventArgs e)
        {
            TL.Content = "";
            TL.IsEnabled = true;
            TM.Content = "";
            TM.IsEnabled = true;
            TR.Content = "";
            TR.IsEnabled = true;
            ML.Content = "";
            ML.IsEnabled = true;
            MM.Content = "";
            MM.IsEnabled = true;
            MR.Content = "";
            MR.IsEnabled = true;
            BL.Content = "";
            BL.IsEnabled = true;
            BM.Content = "";
            BM.IsEnabled = true;
            BR.Content = "";
            BR.IsEnabled = true;
            turn = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameMatrix[i][j] = CellState.Empty;
                }
            }
        }

        private void aiTurn()
        {
            Random rnd = new Random();
            
            bool hasMove = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameMatrix[i][j] == CellState.Empty)
                    {
                        hasMove = true;
                        break;
                    }
                }
                if (hasMove) break;
            }

            if (!hasMove)
            {
                win("Draw");
                return; // No moves available, end turn
            }
            
            if (diffuculty == 1) // easy = purely random 
            {
                easyMove(rnd);
            }

            if (diffuculty == 2) // medium = half random half algorithm
            {
                if (mcount % 2 == 0)
                {
                    easyMove(rnd);
                }
                else
                {
                    hardMove();
                }
                mcount++;
            }

            if (diffuculty == 3) // hard impossible using minmax
            {
                hardMove();
            }
            
            
            turn++;
        }

        private void easyMove(Random rnd)
        {
            bool done = false;
            while (!done)
            {
                int a = rnd.Next(0, 3);
                int b = rnd.Next(0, 3);
                if (gameMatrix[a][b] == CellState.Empty)
                {
                    gameMatrix[a][b] = 0;
                    buttonMatrix[a][b].Content = "O";
                    buttonMatrix[a][b].IsEnabled = false;
                    done = true;
                    if (CheckWin(a, b, CellState.PlayerO))
                    {
                        win("O");
                        return;
                    }
                }
            }
        }

        private void hardMove()
        {
            var result = minmax(gameMatrix, false);
            var move = result.Item2;
            if (move == null || !move.Item1.HasValue || !move.Item2.HasValue)
            {
                // If no valid move is returned, declare a draw and return.
                win("Draw");
                return;
            }
            gameMatrix[move.Item1.Value][move.Item2.Value] = CellState.PlayerO;
            buttonMatrix[move.Item1.Value][move.Item2.Value].Content = "O";
            buttonMatrix[move.Item1.Value][move.Item2.Value].IsEnabled = false;
            if (CheckWin(move.Item1.Value,move.Item2.Value, CellState.PlayerO))
            {
                win("O");
                return;
            }
        }
        private void Back_OnClick(object? sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }


        private void ClosePopUp_OnClick(object? sender, RoutedEventArgs e)
        {
            WinnerPopup.IsOpen = false;
        }

        private Tuple<int,Tuple<int?, int?>> minmax(List<List<CellState>> board, bool maximising)
        {
            // base cases 
            
            // if x wins
            if (CheckWinALL(CellState.PlayerX)) // player 1 maximiing 
            {
                Console.Write("x won");
                return new Tuple<int, Tuple<int?, int?>>(1, new Tuple<int?, int?>(null, null)); // eval pos1, coords in pos2
            }
            
            // if o wins
            if (CheckWinALL(CellState.PlayerO)) // player 2 minimising 
            {
                Console.Write("O won");
                return new Tuple<int, Tuple<int?, int?>>(-1, new Tuple<int?, int?>(null, null));
            }
            
            // if draw
            if (CheckDraw())
            {
                Console.Write("Draw won");
                return new Tuple<int, Tuple<int?, int?>>(0, new Tuple<int?, int?>(null, null));
            }
            
            // alogorithm here
            if (maximising)
            {
                int maxEval = -100;
                Tuple<int?,int?> bestmove = null;
                List<Tuple<int, int>> emptySquares = getEmptySquares(board);

                foreach (var pair in emptySquares)
                {
                    List<List<CellState>> boardCopy = CloneBoard(board);
                    boardCopy[pair.Item1][pair.Item2] = CellState.PlayerX;
                    int eval = minmax(boardCopy, false).Item1;
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        bestmove = new Tuple<int?, int?>(pair.Item1, pair.Item2);
                    }
                }

                return new Tuple<int, Tuple<int?, int?>>(maxEval, bestmove);
            }
            else
            {
                int minEval = 100;
                Tuple<int?,int?> bestmove = new Tuple<int?, int?>(null, null);
                List<Tuple<int, int>> emptySquares = getEmptySquares(board);

                foreach (var pair in emptySquares)
                {
                    List<List<CellState>> boardCopy = CloneBoard(board);
                    boardCopy[pair.Item1][pair.Item2] = CellState.PlayerO;
                    int eval = minmax(boardCopy, true).Item1;
                    if (eval < minEval)
                    {
                        minEval = eval;
                        bestmove = new Tuple<int?, int?>(pair.Item1, pair.Item2);
                    }
                }

                return new Tuple<int, Tuple<int?, int?>>(minEval, bestmove);
            }
            
        }

        private List<Tuple<int, int>> getEmptySquares(List<List<CellState>> board)
        {
            List<Tuple<int, int>> emptySquares = new List<Tuple<int, int>>(); // tuple = pair in c++
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] == CellState.Empty)
                    {
                        emptySquares.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return emptySquares;
        }
        
        private List<List<CellState>> CloneBoard(List<List<CellState>> original)
        {
            var clone = new List<List<CellState>>();
            foreach (var row in original)
            {
                clone.Add(new List<CellState>(row));
            }
            return clone;
        }
        
        
        protected override void OnClosing(WindowClosingEventArgs e)
        {
            base.OnClosing(e);
            // Ensure the application exits fully
            Environment.Exit(0);
        }

}