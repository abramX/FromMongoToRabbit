using System.Collections.Generic;
using System.Threading.Tasks;
using FromMongoToRabbit.MongoDB.Models;

namespace FromMongoToRabbit.MongoDB
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