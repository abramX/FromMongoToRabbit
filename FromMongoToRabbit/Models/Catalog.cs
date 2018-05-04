using System;

namespace FromMongoToRabbit.MongoDB.Models
{
    public class Catalog
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Double Price { get; set; }

        public Catalog()
        {
            StartingDate = DateTime.Now;
            ExpirationDate = StartingDate.AddDays(356);

        }
    }
}
