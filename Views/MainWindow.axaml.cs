using System.Diagnostics;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BookRP.Models;
using BookRP.Services;
using BookRP.ViewModels;
using DiscordRPC;

namespace BookRP;

public partial class MainWindow : Window
{
    private readonly Discord _discord;
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();

        _discord = new Discord();
        _discord.Initialize();
    }

    private void ApplyButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!ValidateForm()) return;
        if (DataContext is not MainViewModel vm) return;

        var classification = vm.SelectedClassification?.Code ?? "000";
        
        var book = new Book
        {
            Title = BookTitleTextBox.Text,
            Author = AuthorTextBox.Text,
            Translator = TranslatorTextBox.Text,
            Classification = classification
        };
        
        _discord.UpdatePresence(CreateRichPresence(book));
    }

    private void RemoveButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _discord.RemovePresence();
    }

    private bool ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(BookTitleTextBox.Text))
        {
            Debug.WriteLine("Err: Book Title is null or whitespace");
            return false;
        }

        // ReSharper disable once InvertIf
        if (string.IsNullOrWhiteSpace(AuthorTextBox.Text))
        {
            Debug.WriteLine("Err: Author is null or whitespace");
            return false;
        }

        return true;
    }

    private static RichPresence CreateRichPresence(Book book)
    {
        var classificationName = Classification.List
            .FirstOrDefault(c => c.Code == book.Classification)?.Name ?? "총류";

        var state = $"{book.Author} 지음";
        if (!string.IsNullOrWhiteSpace(book.Translator))
            state += $"| {book.Translator} 옮김";

        return new RichPresence
        {
            Type = ActivityType.Watching,
            Details = book.Title,
            State = state,
            Assets = new Assets
            {
                LargeImageKey = book.Classification,
                LargeImageText = classificationName,
            }
        };
    }
}