using System;
using System.Reflection;
using log4net;
using Polly;

namespace AppManager
{
    public class ConsumerEngine: IConsumerEngine
    {
        readonly IConsumerServiceReceiver _receiver;
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ConsumerEngine(IConsumerServiceReceiver receiver)
        {
            _receiver = receiver;
        }
        public void Execute()
        {
            var policy = Policy.Handle<Exception>().WaitAndRetry(
                new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(20)
                },
                (exeption, timeSpan) =>
                {
                    Console.WriteLine("Errore nella ricezione: " + exeption.Message);
                    _log.Error("Errore nella ricezione: " + exeption);
                }
            );

            policy.Execute(() =>
            {
                _receiver.ExecuteReceiver();
                _log.Info("Receiving the sent producs");
            });
            _receiver.ExecuteReceiver();
        }
    }
}
