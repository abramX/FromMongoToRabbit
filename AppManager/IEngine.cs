namespace RabbitSender
{
    public interface IEngine
    {
        void Execute();
        void FillMongoDb();
    }
}