using MongoDB.Driver;

namespace FromMongoToRabbit.MongoDB
{
    public interface IMongoDbClient
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}