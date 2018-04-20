using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace FromMongoToRabbit
{
    public class Product
    {
        public ObjectId Id { get; private set; }
        public string Name { get; set;}
        public string Description { get; set; }
        public double Price { get; set; }
        public Catalog PriceList { get; set; }
    }
}
