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
        IConsumerServiceReceiver _receiver;

        public ConsumerEngine(IDbRepository mongo, IConsumerServiceReceiver receiver)
        {
            _mongo = mongo;
            _receiver = receiver;
        }
        public void Execute()
        {
            _receiver.executeReceiver();
        }
    }
}
