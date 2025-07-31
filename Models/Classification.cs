using System.Collections.Generic;

namespace BookRP.Models;

public class ClassificationItem
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; init; } = "000";
}

public static class Classification
{
    public static List<ClassificationItem> List { get; } =
    [
        new() { Name = "총류", Code = "000"},
        new() { Name = "철학", Code = "100"},
        new() { Name = "종교", Code = "200"},
        new() { Name = "사회과학", Code = "300"},
        new() { Name = "자연과학", Code = "400"},
        new() { Name = "기술과학", Code = "500"},
        new() { Name = "예술", Code = "600"},
        new() { Name = "언어", Code = "700"},
        new() { Name = "문학", Code = "800"},
        new() { Name = "역사", Code = "900"},
    ];
}