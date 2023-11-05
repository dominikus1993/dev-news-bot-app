using MongoDB.Driver;

namespace DevNewsBot.App.Infrastructure.MongoDb;

public static class MongoClientExtensions
{
    public static IMongoDatabase GetArticlesDatabase(this IMongoClient client)
    {
        return client.GetDatabase("Articles");
    }
}