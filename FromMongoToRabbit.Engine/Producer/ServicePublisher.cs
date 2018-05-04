using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;
using log4net;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FromMongoToRabbit.Engine.Producer
{
    public class ServicePublisher : IServicePublisher
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void RunService<T>(IEnumerable<T> listToSend)
        {
            var hostaName = ConfigurationManager.AppSettings["RabbitHostName"];
            var userName = ConfigurationManager.AppSettings["RabbitUserName"];
            var password = ConfigurationManager.AppSettings["RabbitPassword"];
            var exchangeName = ConfigurationManager.AppSettings["RabbitExchangeName"];
            _log.Info("[x] Sending products: ");

            var factory = new ConnectionFactory {HostName = hostaName, UserName = userName, Password = password};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(
                    exchangeName,
                    "fanout",
                    false,
                    false);
                var message = JsonConvert.SerializeObject(listToSend);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchangeName,
                    "",
                    null,
                    body);
                _log.Info("Sent products: " + message);
                Console.WriteLine(" [x] Sent ");
            }
        }
    }
}
