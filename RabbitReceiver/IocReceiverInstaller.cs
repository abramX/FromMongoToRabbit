using Ninject.Modules;
using FromMongoToRabbit;
using System.Configuration;

namespace RabbitReceiver
{
    public class IocReceiverInstaller : NinjectModule
    {
        public override void Load()
        {
            KernelInstance.Bind<IDbRepository>().To<MongoRepository>().InSingletonScope()
                   .WithConstructorArgument("connectionString", "mongodb://localhost:27017")
                   .WithConstructorArgument("dbName", ConfigurationManager.AppSettings["MongoDbName"])
                   .WithConstructorArgument("dbCollection", ConfigurationManager.AppSettings["MongoDbCollection"]);
                 
            KernelInstance.Bind<IConsumerEngine>().To<ConsumerEngine>().InSingletonScope();
            KernelInstance.Bind<IConsumerServiceReceiver>().To<ConsumerServiceReceiver>().InSingletonScope();
        }
    }
}
