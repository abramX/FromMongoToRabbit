using FromMongoToRabbit;
using System.Collections.Generic;
using log4net;

namespace RabbitSender
{
    public class Engine : IEngine
    {
        private IProductDbSender _mongo;
        private IServicePublisher Publisher;
        private ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Engine(IProductDbSender mongoRepositorySender, IServicePublisher publisher)
        {
            _mongo = mongoRepositorySender;
            Publisher = publisher;
        }
        public void Execute()
        {
            Log.Info("Starting Sending Process");
            Publisher.RunService(_mongo.GetUnprocessed());

            _mongo.MarkAsProcessed(_mongo.GetUnprocessed());
            Log.Info("Marking as sent the sent producs");
        } 
        
    }
}
