using log4net;
using Quartz;

namespace AppManager
{
    public class SendMesssageJob : IJob
    {
        IEngine _producer;
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SendMesssageJob(IEngine producer)
        {
            _producer = producer;
        }

        public void Execute(IJobExecutionContext context)
        {
            _log.Info("Restarting the scheduled sending job!");
            _producer.Execute();
            
        }
    }
}

