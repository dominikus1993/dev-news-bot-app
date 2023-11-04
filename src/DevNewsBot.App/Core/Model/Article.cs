namespace DevNewsBot.App.Core.Model;

public sealed class Article
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public Uri Link { get; set; }
    public string Content { get; set; } = null!;
    public string Source { get; set; }
    public DateTime CrawledAt { get; set; }
}