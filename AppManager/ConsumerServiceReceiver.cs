using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using FromMongoToRabbit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using log4net;

namespace RabbitSender
{
    public class ConsumerServiceReceiver: IConsumerServiceReceiver
    {
        private IProductDbReceiver _mongo;
        private ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ConsumerServiceReceiver(IProductDbReceiver mongoRepositoryReceiver) {
            _mongo = mongoRepositoryReceiver;
        }

        public void executeReceiver()
        {
            var hostaName = ConfigurationManager.AppSettings["RabbitHostName"];
            var userName = ConfigurationManager.AppSettings["RabbitUserName"];
            var password = ConfigurationManager.AppSettings["RabbitPassword"];
            var exchangeName = ConfigurationManager.AppSettings["RabbitExchangeName"];
            var queueName = ConfigurationManager.AppSettings["RabbitQueueName"];

            try
            {
                var factory = new ConnectionFactory() { HostName = hostaName, UserName = userName, Password = password };
                IList<Product> message;
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

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
                    message = JsonConvert.DeserializeObject<IList<Product>>(Encoding.UTF8.GetString(body));
                    Log.Info("Received " + message.Count.ToString()+ "Message");
                    _mongo.Save(message);
                    Log.Info("Messages Saved");

                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: queueName,
                                                     autoAck: true,
                                                     consumer: consumer);
            }catch(Exception e)
            {
                Console.WriteLine("Errore nella ricezione: " + e.Message);
                Log.Error("Errore nella ricezione: " + e.Message);
            }

        }
    }
}
