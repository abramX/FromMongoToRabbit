using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSender
{
    public class ConsumerService
    {
        public ConsumerService(IConsumerEngine engine)
        {
            Engine = engine;
        }

        public IConsumerEngine Engine { get; }

        public void Start()
        {
            Engine.Execute();
        }
    }
}
