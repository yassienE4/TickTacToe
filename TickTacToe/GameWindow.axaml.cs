using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;

namespace TickTacToe;

public partial class GameWindow : Window
{
    public GameWindow()
    {
        InitializeComponent();
    }
    private int turn = 0;

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
            string player = (turn % 2 == 0) ? "X" : "O";
            TL.Content = player;
            turn++;

            // Check for win conditions
            if (TL.Content == TM.Content && TL.Content == TR.Content) { win(player); }
            if (TL.Content == MM.Content && TL.Content == BL.Content) { win(player); }
            if (TL.Content == ML.Content && TL.Content == BR.Content) { win(player); }

            var b = sender as Button;
            b.IsEnabled = false;
        }

        private void TM_OnClick(object? sender, RoutedEventArgs e)
        {
            string player = (turn % 2 == 0) ? "X" : "O";
            TM.Content = player;
            turn++;

            // Check for win conditions
            if (TL.Content == TM.Content && TM.Content == TR.Content) { win(player);}
            if (TM.Content == MM.Content && TM.Content == BM.Content) { win(player); }

            var b = sender as Button;
            b.IsEnabled = false;
        }

        private void TR_OnClick(object? sender, RoutedEventArgs e)
        {
            string player = (turn % 2 == 0) ? "X" : "O";
            TR.Content = player;
            turn++;

            // Check for win conditions
            if (TR.Content == TM.Content && TR.Content == TL.Content) { win(player); }
            if (TR.Content == MR.Content && TR.Content == BR.Content) { win(player); }
            if (TR.Content == MM.Content && TR.Content == BL.Content) { win(player); }

            var b = sender as Button;
            b.IsEnabled = false;
        }

        private void ML_OnClick(object? sender, RoutedEventArgs e)
        {
            string player = (turn % 2 == 0) ? "X" : "O";
            ML.Content = player;
            turn++;
            
            // Check for win conditions
            if (ML.Content == TL.Content && ML.Content == BL.Content) { win(player); }
            if (ML.Content == MM.Content && ML.Content == MR.Content) { win(player); }

            var b = sender as Button;
            b.IsEnabled = false;
        }

        private void MM_OnClick(object? sender, RoutedEventArgs e)
        {
            string player = (turn % 2 == 0) ? "X" : "O";
            MM.Content = player;
            turn++;

            // Check for win conditions
            if (MM.Content == TL.Content && MM.Content == BR.Content) { win(player); }
            if (MM.Content == TM.Content && MM.Content == BM.Content) { win(player); }
            if (MM.Content == ML.Content && MM.Content == MR.Content) { win(player); }
            if (MM.Content == TR.Content && MM.Content == BL.Content) { win(player); }


            var b = sender as Button;
            b.IsEnabled = false;
        }

        private void MR_OnClick(object? sender, RoutedEventArgs e)
        {
            string player = (turn % 2 == 0) ? "X" : "O";
            MR.Content = player;
            turn++;

            // Check for win conditions
            if (MR.Content == TR.Content && MR.Content == BR.Content) { win(player); }
            if (MR.Content == MM.Content && MR.Content == ML.Content) { win(player); }

            var b = sender as Button;
            b.IsEnabled = false;
        }

        private void BL_OnClick(object? sender, RoutedEventArgs e)
        {
            string player = (turn % 2 == 0) ? "X" : "O";
            BL.Content = player;
            turn++;

            // Check for win conditions
            if (BL.Content == TL.Content && BL.Content == ML.Content) { win(player); }
            if (BL.Content == MM.Content && BL.Content == TR.Content) { win(player); }
            if (BL.Content == BM.Content && BL.Content == BR.Content) { win(player); }


            var b = sender as Button;
            b.IsEnabled = false;
        }

        private void BM_OnClick(object? sender, RoutedEventArgs e)
        {
            string player = (turn % 2 == 0) ? "X" : "O";
            BM.Content = player;
            turn++;

            // Check for win conditions
            if (BM.Content == TM.Content && BM.Content == MM.Content) { win(player); }
            if (BM.Content == BL.Content && BM.Content == BR.Content) { win(player);}

            var b = sender as Button;
            b.IsEnabled = false;
        }

        private void BR_OnClick(object? sender, RoutedEventArgs e)
        {
            string player = (turn % 2 == 0) ? "X" : "O";
            BR.Content = player;
            turn++;

            // Check for win conditions
            if (BR.Content == TR.Content && BR.Content == MR.Content) { win(player); }
            if (BR.Content == BL.Content && BR.Content == BM.Content) { win(player); }
            if (BR.Content == MM.Content && BR.Content == TL.Content) { win(player); }

            var b = sender as Button;
            b.IsEnabled = false;
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
        }

        private void Back_OnClick(object? sender, RoutedEventArgs e)
        {
            Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
}