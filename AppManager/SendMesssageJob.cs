using Quartz;
using System;

namespace RabbitSender
{
    public class SendMesssageJob : IJob
    {
        IEngine _producer;

        public SendMesssageJob(IEngine producer)
        {
            _producer = producer;
        }

        public void Execute(IJobExecutionContext context)
        {
            _producer.Execute();          
        }
    }
}

