using System;

namespace RabbitSender
{
    public class AppMangerService
    {
        private IEngine _engine;
        public AppMangerService(IEngine engine)
        {
            _engine = engine;
        }

        

        public void Start()
        {
            _engine.FillMongoDb();
            _engine.Execute();
        }
    }
}