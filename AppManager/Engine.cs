using System;
using System.Reflection;
using System.Threading.Tasks;
using FromMongoToRabbit;
using log4net;
using Polly;

namespace AppManager
{
    public class Engine : IEngine
    {
        private readonly IProductDbSender _mongo;
        private readonly IServicePublisher _publisher;
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Engine(IProductDbSender mongoRepositorySender, IServicePublisher publisher)
        {
            _mongo = mongoRepositorySender;
            _publisher = publisher;
        }

        public async Task Execute()
        {
            _log.Info("Starting Sending Process");


            var unprocessedProducts = await _mongo.GetUnprocessed();
            var policy = Policy.Handle<Exception>().WaitAndRetry(
                new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(20)
                },
                (exeption, timeSpan) =>
                {
                    _log.Error("CErrore nella spedizione:" + exeption);
                    Console.WriteLine("Errore nella spedizione: " + exeption.Message);
                }
            );

            policy.Execute(() =>
            {
                _publisher.RunService(unprocessedProducts);
                _mongo.MarkAsProcessed(unprocessedProducts);
                _log.Info("Marking as sent the sent producs");
            });
        }
    }
}