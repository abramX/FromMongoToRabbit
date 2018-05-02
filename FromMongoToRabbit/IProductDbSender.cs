using System.Collections.Generic;

namespace FromMongoToRabbit
{
    public interface IProductDbSender
    {
        void Save(Product product);


        void Save(IList<Product> products);


        void MarkAsProcessed(IEnumerable<Product> products);


        IList<Product> GetUnprocessed();

        void FillMongoDb();

    }
}