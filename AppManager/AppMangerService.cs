
using System;
using System.Web.Http;

namespace RabbitSender
{
    public class AppMangerService
    {
        private IEngine _engine;
        private IConsumerEngine _consumerEngine;
        public AppMangerService(IEngine engine, IConsumerEngine consumerEngine)
        {
            _engine = engine;
            _consumerEngine = consumerEngine;
        }

        public void Start()
        {  
            _engine.Execute();
            _consumerEngine.Execute();
            
        }
    }
}