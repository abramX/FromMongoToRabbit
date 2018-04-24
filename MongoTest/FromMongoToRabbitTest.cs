using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitSender;
using RabbitReceiver;
using FromMongoToRabbit;


namespace MongoTest
{
    [TestFixture]
    class FromMongoToRabbitTest
    {
        [TestCase]
        void MessageIsSent()
        {
            IList<Product> listaProdotti;
            for(var i = 0; i < 30; i++)
            {
                Product p = new Product {
                    Name="prodotto"+i,
                    Description="descrizione"+i,
                    Price=300.00,

                };
                //listaProdotti.Add(p);
            }
            
        }
    }
}
