using System.Collections.Generic;

namespace FromMongoToRabbit
{
    public interface IProductDbReceiver
    {
        void Save(Product product);


        void Save(IList<Product> products);


        void MarkAsProcessed(IList<Product> products);


        IList<Product> GetUnprocessed();
    }
}