﻿namespace AppManager
{
    public class AppMangerService
    {
        private readonly IEngine _engine;
        private readonly IConsumerEngine _consumerEngine;
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