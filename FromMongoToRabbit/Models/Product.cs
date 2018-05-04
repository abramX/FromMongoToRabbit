using System;
using MongoDB.Bson;

namespace FromMongoToRabbit.MongoDB.Models
{
    public class Product
    {
        public ObjectId Id { get; private set; }
        public string Name { get; set;}
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public Catalog PriceList { get; set; }
        public bool Sent { get; set; }
        public Product()
        {
            CreationDate = DateTime.Now;
            Sent = false;
        }
    }
}
