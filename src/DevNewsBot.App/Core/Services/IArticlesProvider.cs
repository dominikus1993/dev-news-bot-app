using DevNewsBot.App.Core.Model;

namespace DevNewsBot.App.Core.Services;

public sealed class PagedArticles(
    IEnumerable<Article> articles,
    long currentPage,
    long quantity,
    long totalPages,
    long totalArticlesQuantity)
{
    public IEnumerable<Article> Articles { get; } = articles;
    public long CurrentPage { get; } = currentPage;
    public long Quantity { get; } = quantity;
    public long TotalPages { get; } = totalPages;
    public long TotalArticlesQuantity { get; } = totalArticlesQuantity;
}

public sealed record ArticlesQuery(long Page, long PageSize);
public interface IArticlesProvider
{
    Task<PagedArticles> GetArticles(ArticlesQuery query, CancellationToken cancellationToken = default);
}

public sealed class FakeArticlesProvider : IArticlesProvider
{
    public Task<PagedArticles> GetArticles(ArticlesQuery query, CancellationToken cancellationToken = default)
    {
        var articles = new List<Article>()
        {
            new Article()
            {
                Content = "x - content", Id = Guid.NewGuid().ToString("D"), Link = new Uri("http://www.google.pl"),
                Source = "google", Title = $"some title {query.Page}", CrawledAt = DateTime.UtcNow
            }
        };
        return Task.FromResult(new PagedArticles(articles, query.Page, 1, query.Page + 1, query.PageSize + 10001));
    }
}