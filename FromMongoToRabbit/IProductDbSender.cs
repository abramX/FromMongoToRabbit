using System.Collections.Generic;
using System.Threading.Tasks;

namespace FromMongoToRabbit
{
    public interface IProductDbSender
    {
        Task Save(Product product);


        Task Save(IList<Product> products);


        Task MarkAsProcessed(IEnumerable<Product> products);


        Task<IList<Product>> GetUnprocessed();

        Task FillMongoDb();

    }
}