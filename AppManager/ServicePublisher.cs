using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace AppManager
{
    class ServicePublisher : IServicePublisher
    {
        public void RunService<T>(IEnumerable<T> listToSend) 
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "FirstQueueIbra",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(listToSend);
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
