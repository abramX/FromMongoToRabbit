using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FromMongoToRabbit;
using Newtonsoft;

namespace RabbitReceiver
{
    public class ConsumerEngine: IConsumerEngine
    {
        IDbRepository _mongo;
        IConsumerServicePublisher _publisher;

        public ConsumerEngine(IDbRepository mongo, IConsumerServicePublisher publisher)
        {
            _mongo = mongo;
            _publisher = publisher;
        }
        public void Execute()
        {
            _publisher.executePublisher();
        }
    }
}
