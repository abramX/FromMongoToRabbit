using Quartz;
using System;
using RabbitSender;
using System.Threading.Tasks;

namespace MainApp

{
    public class MessageSendJob : IJob
    {
        IEngine _producer;

        public MessageSendJob(IEngine producer)
        {
            _producer = producer;
        }

        public void Execute(IJobExecutionContext context)
        {
            _producer.Execute();          
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}

