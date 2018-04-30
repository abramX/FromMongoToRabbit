using MongoDB.Driver;

namespace FromMongoToRabbit
{
    public class MongoDbClient
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbClient(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);
            _mongoDatabase = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _mongoDatabase.GetCollection<T>(collectionName);
        }
    }

    
}
