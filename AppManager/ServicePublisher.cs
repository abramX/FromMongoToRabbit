using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;
using System.Configuration;

namespace AppManager
{
    public class ServicePublisher : IServicePublisher
    {
        public void RunService<T>(IEnumerable<T> listToSend) 
        {
            var hostaName = ConfigurationManager.AppSettings["RabbitHostName"];
            var userName = ConfigurationManager.AppSettings["RabbitUserName"];
            var password = ConfigurationManager.AppSettings["RabbitPassword"];
            var exchangeName = ConfigurationManager.AppSettings["RabbitExchangeName"];
            var queueName = ConfigurationManager.AppSettings["RabbitQueueName"];

            var factory = new ConnectionFactory() { HostName = hostaName, UserName = userName, Password = password };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare(queue: queueName,
                //                     durable: false,
                //                     exclusive: false,
                //                     autoDelete: false,
                //                     arguments: null);

                string message = JsonConvert.SerializeObject(listToSend);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchangeName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

        }
    }
}
