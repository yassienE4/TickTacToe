using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
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
            }
            if(x == "O")
            {
                Winner.Text = "Winner= O";
            }
            if(x == "Draw")
            {
                Winner.Text = "DRAW!";
            }
        }

        private bool CheckWin(int row, int col, CellState player)
        {
            // Check the row of the last move
            if (gameMatrix[row][0] == player && gameMatrix[row][1] == player && gameMatrix[row][2] == player)
                return true;

            // Check the column of the last move
            if (gameMatrix[0][col] == player && gameMatrix[1][col] == player && gameMatrix[2][col] == player)
                return true;

            // Check the main diagonal if applicable
            if (row == col && gameMatrix[0][0] == player && gameMatrix[1][1] == player && gameMatrix[2][2] == player)
                return true;

            // Check the anti-diagonal if applicable
            if (row + col == 2 && gameMatrix[0][2] == player && gameMatrix[1][1] == player && gameMatrix[2][0] == player)
                return true;

            return false;
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
            
        }
        private void Back_OnClick(object? sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        
}