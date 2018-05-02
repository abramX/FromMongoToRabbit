namespace AppManager
{
    public class ConsumerEngine: IConsumerEngine
    {
        readonly IConsumerServiceReceiver _receiver;

        public ConsumerEngine(IConsumerServiceReceiver receiver)
        {
            _receiver = receiver;
        }
        public void Execute()
        {
            _receiver.ExecuteReceiver();
        }
    }
}
