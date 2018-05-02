using MongoDB.Driver;

namespace FromMongoToRabbit
{
    public interface IMongoDbClient
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}