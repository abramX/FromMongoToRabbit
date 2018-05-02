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
