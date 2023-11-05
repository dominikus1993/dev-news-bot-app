using DevNewsBot.App.Core.Model;

namespace DevNewsBot.App.ViewModel;

public sealed class ArticlesViewModel
{
    public IReadOnlyList<Article>? Articles { get; set; }
    public int ArticlesQuantity { get; set; }
    public int Cursor { get; set; }
    public long Page { get; set; }
    public long PageSize { get; set; }
}