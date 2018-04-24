using FromMongoToRabbit;
using System.Collections.Generic;

namespace RabbitSender
{
    public class Engine : IEngine
    {
        private IDbRepository _mongo;
        private IServicePublisher Publisher;

        public Engine(IDbRepository mongoRepository, IServicePublisher publisher)
        {
            _mongo = mongoRepository;
            Publisher = publisher;
        }

        public void Execute()
        {
            Publisher.RunService(_mongo.All<Product>());
        }
        public void FillMongoDb()
        {
            
            //for (var i = 0; i < 50; i++)
            //{
            //    Product p = new Product
            //    {
            //        Name = "prodotto" + i,
            //        Description = "descrizione" + i,
            //        PriceList=new Catalog
            //        {
            //            Name="Catalogo 1",
            //            Description="Catalago principale con i prezzi base",
            //            Price=i*10
            //        }

            //    };
            //    _mongo.Add<Product>(p);
            //}
        }
    }
}
