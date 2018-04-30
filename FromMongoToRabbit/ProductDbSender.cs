using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace FromMongoToRabbit
{
    public class ProductDbSender: IProductDbSender
    {
        private readonly IMongoCollection<Product> _mongoCollection;

        public ProductDbSender(MongoDbClient mongoDbClient)
        {
            _mongoCollection = mongoDbClient.GetCollection<Product>("products");
        }

        public void Save(Product product)
        {
            _mongoCollection.InsertOne(product);
        }
        public void Save(IList<Product> products)
        {
            foreach(Product p in products)
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

        public void FillMongoDb()
        {

            for (var i = 0; i < 50; i++)
            {
                Product p = new Product
                {
                    Name = "prodotto" + i,
                    Description = "descrizione" + i,
                    PriceList = new Catalog
                    {
                        Name = "Catalogo 1",
                        Description = "Catalago principale con i prezzi base",
                        Price = i * 10
                    }
                };
                Save(p);
            }
        }
    }
}
