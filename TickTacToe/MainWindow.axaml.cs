using System;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TickTacToe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AIGAME.IsEnabled = false;
        }

        private void onNewGame(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow();
            Hide();
            window.Show();
            
        }

        private void Quit_OnClick(object? sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
