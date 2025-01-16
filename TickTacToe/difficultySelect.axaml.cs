using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace TickTacToe;

public partial class difficultySelect : Window
{
    public difficultySelect()
    {
        InitializeComponent();
    }

    private void Easy_OnClick(object? sender, RoutedEventArgs e)
    {
        AiGameWindow window = new AiGameWindow(1);
        Hide();
        window.Show();
    }

    private void Medium_OnClick(object? sender, RoutedEventArgs e)
    {
        AiGameWindow window = new AiGameWindow(2);
        Hide();
        window.Show();
    }

    private void Hard_OnClick(object? sender, RoutedEventArgs e)
    {
        AiGameWindow window = new AiGameWindow(3);
        Hide();
        window.Show();
    }
    
    protected override void OnClosing(WindowClosingEventArgs e)
    {
        base.OnClosing(e);
        // Ensure the application exits fully
        Environment.Exit(0);
    }

}