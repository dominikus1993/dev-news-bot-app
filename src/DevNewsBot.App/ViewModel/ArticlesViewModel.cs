using DevNewsBot.App.Core.Model;

namespace DevNewsBot.App.ViewModel;

public sealed class ArticlesViewModel
{
    public required IReadOnlyList<Article>? Articles { get; init; }
    public required int ArticlesQuantity { get; init; }
    public required int Cursor { get; init; }
    public required long Page { get; init; }
    public required long PageSize { get; init; }
}