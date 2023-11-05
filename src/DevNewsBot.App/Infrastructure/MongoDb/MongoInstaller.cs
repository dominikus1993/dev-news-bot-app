using DevNewsBot.App.Core.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DevNewsBot.App.Infrastructure.MongoDb;

public static class MongoInstaller
{
    public static void Setup()
    {
        BsonClassMap.RegisterClassMap<Article>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapIdField(a => a.Id);
        });
    }

    public static WebApplicationBuilder AddMongoDb(this WebApplicationBuilder builder)
    {
        var client = new MongoClient(builder.Configuration.GetConnectionString("Articles"));
        builder.Services.AddSingleton<IMongoClient>(client);
        builder.Services.AddSingleton<IMongoDatabase>(client.GetArticlesDatabase());
        return builder;
    }
}