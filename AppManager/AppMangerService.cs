using System;

namespace AppManager
{
    internal class AppMangerService
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