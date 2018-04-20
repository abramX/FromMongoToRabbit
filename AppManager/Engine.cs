using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FromMongoToRabbit;
using RabbitMQ.Client;

namespace AppManager
{
    class Engine : IEngine
    {
        private IDbRepository _mongo;

        public Engine(IDbRepository mongoRepository)
        {
            _mongo = mongoRepository;
        }
        public void Execute()
        {
            IList<Product> productList = _mongo.All<Product>().ToList();

            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "FirstQueueIbra",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = productList.First().Description.ToString();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "IbraExchange",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

        }
    }
}
