namespace FromMongoToRabbit
{
    public class MongoDbClientReceiver : MongoDbClient
    {
        public MongoDbClientReceiver(string connectionString) : base(connectionString)
        {
        }
    }
}
