using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace FromMongoToRabbit
{
    public class ProductDbReceiver: IProductDbReceiver
    {
        private readonly IMongoCollection<Product> _mongoCollection;

        public ProductDbReceiver(IMongoDbClient mongoDbClient)
        {
            _mongoCollection = mongoDbClient.GetCollection<Product>("products");
        }

        public void Save(Product product)
        {
            _mongoCollection.InsertOne(product);
        }
        public void Save(IList<Product> products)
        {
            foreach (Product p in products)
            {
                Save(p);
            }

        }

        public void MarkAsProcessed(IList<Product> products)
        {
            var idList = products.Select(s => s.Id).ToList();

            _mongoCollection.UpdateMany(
                    Builders<Product>.Filter.In(s => s.Id, idList),
                    Builders<Product>.Update.Set(s => s.Sent, true)
                );
        }

        public IList<Product> GetUnprocessed()
        {
            return _mongoCollection
                .Find(Builders<Product>.Filter.Where(s => s.Sent == false))
                .ToList();
        }
    }
}
