using FromMongoToRabbit;

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
    }
}
