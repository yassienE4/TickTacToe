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
            //AIGAME.IsEnabled = false;
        }

        private void onNewGame(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow();
            Hide();
            window.Show();
            
        }

        private void onNewAiGame(object sender, RoutedEventArgs e)
        {
            
            difficultySelect d = new difficultySelect();
            Hide();
            d.Show();
        }

        private void Quit_OnClick(object? sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        protected override void OnClosing(WindowClosingEventArgs e)
        {
            base.OnClosing(e);
            // Ensure the application exits fully
            Environment.Exit(0);
        }

    }
}
