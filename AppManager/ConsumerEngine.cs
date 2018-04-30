namespace RabbitSender
{
    public class ConsumerEngine: IConsumerEngine
    {
        
        IConsumerServiceReceiver _receiver;

        public ConsumerEngine(IConsumerServiceReceiver receiver)
        {
            _receiver = receiver;
        }
        public void Execute()
        {
            _receiver.executeReceiver();
        }
    }
}
