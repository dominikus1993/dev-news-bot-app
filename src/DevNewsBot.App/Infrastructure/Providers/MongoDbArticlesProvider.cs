using DevNewsBot.App.Core.Model;
using DevNewsBot.App.Core.Services;
using DevNewsBot.App.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace DevNewsBot.App.Infrastructure.Providers;

public sealed class MongoDbArticlesProvider : IArticlesProvider
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbArticlesProvider(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    public async Task<PagedArticles> GetArticles(ArticlesQuery query, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Article>.Filter.Empty;
        var skip = (query.Page - 1) * query.PageSize;
        var articles = _mongoDatabase.Articles();
        var q = articles.Find(filter);
        
        var countQ = q.CountDocumentsAsync(cancellationToken);
        
        var resQ = q
            .Sort(Builders<Article>.Sort.Descending(x => x.CrawledAt))
            .Skip((int)skip)
            .Limit((int)query.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        await Task.WhenAll(countQ, resQ);
        var count = await countQ;
        var res = await resQ;
        var totalPages = (int)Math.Ceiling(count / (double)query.PageSize);
        return new PagedArticles(res, query.Page, res.Count, totalPages, count);
    }
}