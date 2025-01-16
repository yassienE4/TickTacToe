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
        private int turn = 0;
        public int diffuculty = 0;

        private List<List<int>> gameMatrix = new List<List<int>>()
        {
            new List<int>() { -1, -1, -1, },
            new List<int>() { -1, -1, -1, },
            new List<int>() { -1, -1, -1, },
        };

        private List<List<Button>> buttonMatrix;

        private void win(string x)
        {
            if (x == "X")
            {
                Winner.Text = "Winner= X";
            }
            else
            {
                Winner.Text = "Winner= O";
            }
        }
        private void TL_OnClick(object? sender, RoutedEventArgs e)
        {

            if (turn % 2 == 0)
            {
                TL.Content = "X";
                if (TL.Content == TM.Content && TL.Content == TR.Content) { win("X"); return; }
                if (TL.Content == MM.Content && TL.Content == BL.Content) { win("X"); return; }
                if (TL.Content == ML.Content && TL.Content == BR.Content) { win("X"); return; }

                gameMatrix[0][0] = 1;
                turn++;
                var b = sender as Button;
                b.IsEnabled = false;
                aiTurn();
            }
        }

        private void TM_OnClick(object? sender, RoutedEventArgs e)
        {
            if (turn % 2 == 0)
            {
                TM.Content = "X";
                if (TL.Content == TM.Content && TM.Content == TR.Content) { win("X"); return; }
                if (TM.Content == MM.Content && TM.Content == BM.Content) { win("X"); return; }

                gameMatrix[0][1] = 1;
                turn++;
                aiTurn();
                var b = sender as Button;
                b.IsEnabled = false;
            }
        }

        private void TR_OnClick(object? sender, RoutedEventArgs e)
        {
            if (turn % 2 == 0)
            {
                TR.Content = "X";
                string player = "X";
                if (TR.Content == TM.Content && TR.Content == TL.Content) { win(player);
                    return;
                }
                if (TR.Content == MR.Content && TR.Content == BR.Content) { win(player); return; }
                if (TR.Content == MM.Content && TR.Content == BL.Content) { win(player); return; }

                gameMatrix[0][2] = 1;
                turn++;
                var b = sender as Button;
                b.IsEnabled = false;
                aiTurn();
            }
        }

        private void ML_OnClick(object? sender, RoutedEventArgs e)
        {
            if (turn % 2 == 0)
            {
                ML.Content = "X";
                string player = "X";
                if (ML.Content == TL.Content && ML.Content == BL.Content) { win(player); return; }
                if (ML.Content == MM.Content && ML.Content == MR.Content) { win(player); return; }

                gameMatrix[1][0] = 1;
                turn++;
                aiTurn();
                var b = sender as Button;
                b.IsEnabled = false;
            }
        }

        private void MM_OnClick(object? sender, RoutedEventArgs e)
        {
            
            if (turn % 2 == 0)
            {
                MM.Content = "X";
                string player = "X";
                if (MM.Content == TL.Content && MM.Content == BR.Content) { win(player); return; }
                if (MM.Content == TM.Content && MM.Content == BM.Content) { win(player); return; }
                if (MM.Content == ML.Content && MM.Content == MR.Content) { win(player); return; }
                if (MM.Content == TR.Content && MM.Content == BL.Content) { win(player); return; }

                gameMatrix[1][1] = 1;
                turn++;
                aiTurn();
                var b = sender as Button;
                b.IsEnabled = false;
            }
        }

        private void MR_OnClick(object? sender, RoutedEventArgs e)
        {
            if (turn % 2 == 0)
            {
                MR.Content = "X";
                string player = "X";
                if (MR.Content == TR.Content && MR.Content == BR.Content) { win(player); return; }
                if (MR.Content == MM.Content && MR.Content == ML.Content) { win(player); return; }

                gameMatrix[1][2] = 1;
                turn++;
                aiTurn();
                var b = sender as Button;
                b.IsEnabled = false;
            }
        }

        private void BL_OnClick(object? sender, RoutedEventArgs e)
        {
            if (turn % 2 == 0)
            {
                BL.Content = "X";
                string player = "X";
                if (BL.Content == TL.Content && BL.Content == ML.Content) { win(player); return; }
                if (BL.Content == MM.Content && BL.Content == TR.Content) { win(player); return; }
                if (BL.Content == BM.Content && BL.Content == BR.Content) { win(player); return; }

                gameMatrix[2][0] = 1;
                turn++;
                aiTurn();
                var b = sender as Button;
                b.IsEnabled = false;
            }
        }

        private void BM_OnClick(object? sender, RoutedEventArgs e)
        {
            if (turn % 2 == 0)
            {
                BM.Content = "X";
                string player = "X";
                if (BM.Content == TM.Content && BM.Content == MM.Content) { win(player); return; }
                if (BM.Content == BL.Content && BM.Content == BR.Content) { win(player); return;}

                gameMatrix[2][1] = 1;
                turn++;
                aiTurn();
                var b = sender as Button;
                b.IsEnabled = false;
            }
        }

        private void BR_OnClick(object? sender, RoutedEventArgs e)
        {
            if (turn % 2 == 0)
            {
                BR.Content = "X";
                string player = "X";
                if (BR.Content == TR.Content && BR.Content == MR.Content) { win(player); return; }
                if (BR.Content == BL.Content && BR.Content == BM.Content) { win(player); return; }
                if (BR.Content == MM.Content && BR.Content == TL.Content) { win(player); return; }

                gameMatrix[2][2] = 1;
                turn++;
                aiTurn();
                var b = sender as Button;
                b.IsEnabled = false;
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
                    gameMatrix[i][j] = -1;
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
                    if (gameMatrix[i][j] == -1)
                    {
                        hasMove = true;
                        break;
                    }
                }
                if (hasMove) break;
            }

            if (!hasMove)
            {
                Winner.Text = "Draw!";
                return; // No moves available, end turn
            }
            
            if (diffuculty == 1) // easy = purely random 
            {
                bool done = false;
                while (!done)
                {
                    int a = rnd.Next(0, 3);
                    int b = rnd.Next(0, 3);
                    if (gameMatrix[a][b] == -1)
                    {
                        gameMatrix[a][b] = 0;
                        buttonMatrix[a][b].Content = "O";
                        done = true;
                    }
                }
            }

            if (diffuculty == 2) // medium = half random half algorithm
            {
                
            }

            if (diffuculty == 3) // hard impossible using minmax
            {
                
            }
            
            
            turn++;
        }
        private void Back_OnClick(object? sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        
}