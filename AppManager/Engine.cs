using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using FromMongoToRabbit;
using log4net;

namespace AppManager
{
    public class Engine : IEngine
    {
        private IProductDbSender _mongo;
        private IServicePublisher _publisher;
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Engine(IProductDbSender mongoRepositorySender, IServicePublisher publisher)
        {
            _mongo = mongoRepositorySender;
            _publisher = publisher;
        }
        public void Execute()
        {
            _log.Info("Starting Sending Process");
            
            try
            {
                var unprocessedProducts = _mongo.GetUnprocessed();
                _publisher.RunService(unprocessedProducts);

                _mongo.MarkAsProcessed(unprocessedProducts);
                _log.Info("Marking as sent the sent producs");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _log.Error("Connessione al db fallita!!!:"+e);
            }

            
        } 
        
    }
}
