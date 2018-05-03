using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FromMongoToRabbit
{
    public class ProductDbReceiver: IProductDbReceiver
    {
        private readonly IMongoCollection<Product> _mongoCollection;

        public ProductDbReceiver(IMongoDbClient mongoDbClient)
        {
            _mongoCollection = mongoDbClient.GetCollection<Product>("products");
        }

        public async Task Save(Product product)
        {
            await _mongoCollection.InsertOneAsync(product);
        }
        public async Task Save(IList<Product> products)
        {
            foreach (Product p in products)
            {
                await Save(p);
            }

        }

        public async Task MarkAsProcessed(IList<Product> products)
        {
            var idList = products.Select(s => s.Id).ToList();

            await _mongoCollection.UpdateManyAsync(
                    Builders<Product>.Filter.In(s => s.Id, idList),
                    Builders<Product>.Update.Set(s => s.Sent, true)
                );
        }

        public async Task<IList<Product>> GetUnprocessed()
        {
            return await _mongoCollection
                .Find(Builders<Product>.Filter.Where(s => s.Sent == false))
                .ToListAsync();
        }
    }
}
