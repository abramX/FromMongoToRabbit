using System;

namespace RabbitSender
{
    public class AppMangerService
    {
        public AppMangerService(IEngine engine)
        {
            Engine = engine;
        }

        public IEngine Engine { get; }

        public void Start()
        {
            Engine.Execute();
        }
    }
}