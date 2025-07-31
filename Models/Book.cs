namespace BookRP.Models;

public class Book
{
    public string? Title { get; init; }
    public string? Author { get; init; }
    public string? Translator { get; init; }
    public string Classification { get; init; } = "000";
}