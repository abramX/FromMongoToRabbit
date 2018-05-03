using System.Collections.Generic;
using System.Threading.Tasks;

namespace FromMongoToRabbit
{
    public interface IProductDbReceiver
    {
        Task Save(Product product);


        Task Save(IList<Product> products);


        Task MarkAsProcessed(IList<Product> products);


        Task<IList<Product>> GetUnprocessed();
    }
}