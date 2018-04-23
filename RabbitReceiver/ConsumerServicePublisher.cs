using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using FromMongoToRabbit;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace RabbitReceiver
{
    public class ConsumerServicePublisher: IConsumerServicePublisher
    {
        private IDbRepository _mongo;

        public ConsumerServicePublisher(IDbRepository mongo) {
            _mongo = mongo;
        }

        public void executePublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            IList<Product> message;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "FirstQueueIbra",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    message = JsonConvert.DeserializeObject < IList < Product >> (Encoding.UTF8.GetString(body));
                    _mongo.Add<Product>(message);
                    Console.WriteLine(" [x] Received {0}", message);
                    
                };
                channel.BasicConsume(queue: "FirstQueueIbra",
                                                     autoAck: true,
                                                     consumer: consumer);

                //Console.WriteLine(" Press [enter] to exit.");
                //Console.ReadLine();
                
                
            }

        }
    }
}
