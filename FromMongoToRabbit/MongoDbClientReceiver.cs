using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromMongoToRabbit
{
    public class MongoDbClientReceiver : MongoDbClient
    {
        public MongoDbClientReceiver(string connectionString) : base(connectionString)
        {
        }
    }
}
