using DevNewsBot.App.Core.Model;
using MongoDB.Driver;

namespace DevNewsBot.App.Infrastructure.MongoDb;

public static class MongoDatabaseExtensions
{
    public static IMongoCollection<Article> Articles(this IMongoDatabase database)
    {
        return database.GetCollection<Article>("articles");
    }
}