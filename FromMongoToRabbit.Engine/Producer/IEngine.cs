using System.Threading.Tasks;

namespace FromMongoToRabbit.Engine.Producer
{
    public interface IEngine
    {
        Task Execute();
        
    }
}