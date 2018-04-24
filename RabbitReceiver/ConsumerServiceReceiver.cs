using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using FromMongoToRabbit;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace RabbitReceiver
{
    public class ConsumerServiceReceiver: IConsumerServiceReceiver
    {
        private IDbRepository _mongo;

        public ConsumerServiceReceiver(IDbRepository mongo) {
            _mongo = mongo;
        }

        public void executeReceiver()
        {
            var hostaName = ConfigurationManager.AppSettings["RabbitHostName"];
            var userName = ConfigurationManager.AppSettings["RabbitUserName"];
            var password = ConfigurationManager.AppSettings["RabbitPassword"];
            var exchangeName = ConfigurationManager.AppSettings["RabbitExchangeName"];
            var queueName = ConfigurationManager.AppSettings["RabbitQueueName"];

            var factory = new ConnectionFactory() { HostName = hostaName, UserName = userName, Password = password };
            IList<Product> message;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                channel.QueueBind(queue: queueName,
                  exchange: exchangeName,
                  routingKey: "");
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    message = JsonConvert.DeserializeObject <IList<Product >> (Encoding.UTF8.GetString(body));
                    _mongo.Add<Product>(message);
                    Console.WriteLine(" [x] Received {0}", message);                   
                };
                channel.BasicConsume(queue: queueName,
                                                     autoAck: true,
                                                     consumer: consumer);

                Console.ReadLine();
                                
            }

        }
    }
}
