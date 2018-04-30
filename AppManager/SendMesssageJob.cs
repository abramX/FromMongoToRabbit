using Quartz;
using log4net;

namespace RabbitSender
{
    public class SendMesssageJob : IJob
    {
        IEngine _producer;
        private ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SendMesssageJob(IEngine producer)
        {
            _producer = producer;
        }

        public void Execute(IJobExecutionContext context)
        {
            Log.Info("Restarting the scheduled sending job!");
            _producer.Execute();
            
        }
    }
}

