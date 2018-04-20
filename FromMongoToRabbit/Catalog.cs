using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromMongoToRabbit
{
     public class Catalog
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Catalog()
        {
            StartingDate = DateTime.Now;
        }
    }
}
