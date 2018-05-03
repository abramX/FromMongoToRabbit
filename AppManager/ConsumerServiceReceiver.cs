using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;
using FromMongoToRabbit;
using log4net;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AppManager
{
    public class ConsumerServiceReceiver : IConsumerServiceReceiver
    {
        private readonly IProductDbReceiver _mongo;
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ConsumerServiceReceiver(IProductDbReceiver mongoRepositoryReceiver)
        {
            _mongo = mongoRepositoryReceiver;
        }

        public void ExecuteReceiver()
        {
            var hostaName = ConfigurationManager.AppSettings["RabbitHostName"];
            var userName = ConfigurationManager.AppSettings["RabbitUserName"];
            var password = ConfigurationManager.AppSettings["RabbitPassword"];
            var exchangeName = ConfigurationManager.AppSettings["RabbitExchangeName"];
            var queueName = ConfigurationManager.AppSettings["RabbitQueueName"];


            var factory = new ConnectionFactory {HostName = hostaName, UserName = userName, Password = password};
            IList<Product> message;
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queueName,
                false,
                false,
                false,
                null);
            channel.QueueBind(queueName,
                exchangeName,
                "");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                message = JsonConvert.DeserializeObject<IList<Product>>(Encoding.UTF8.GetString(body));
                _log.Info("Received " + message.Count.ToString() + "Message");
                await _mongo.Save(message);
                _log.Info("Messages Saved");

                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queueName,
                true,
                consumer);
        }
    }
}