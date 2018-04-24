using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FromMongoToRabbit;
using RabbitMQ.Client;

namespace AppManager
{
    public class Engine : IEngine
    {
        private IDbRepository _mongo;

        public Engine(IDbRepository mongoRepository, IServicePublisher publisher)
        {
            _mongo = mongoRepository;
            Publisher = publisher;
        }

        public IServicePublisher Publisher { get; }

        public void Execute()
        {
            Publisher.RunService(_mongo.All<Product>());
        }
    }
}
