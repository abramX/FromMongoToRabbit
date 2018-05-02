using System.Configuration;
using FromMongoToRabbit;
using Ninject.Modules;

namespace AppManager
{
    public class IocInstaller : NinjectModule
    {
        public override void Load()
        {
            

            KernelInstance.Bind<MongoDbClient>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoConnectionSender"].ConnectionString);
           
            KernelInstance.Bind<IProductDbSender>()
               .To<ProductDbSender>()
               .InSingletonScope();

            KernelInstance.Bind<MongoDbClientReceiver>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoConnectionReceiver"].ConnectionString);

            KernelInstance.Bind<IProductDbReceiver>()
               .To<ProductDbReceiver>()
               .InSingletonScope();
            //Producer
            KernelInstance.Bind<IEngine>()
                .To<Engine>()
                .InSingletonScope();

            KernelInstance.Bind<IServicePublisher>()
                .To<ServicePublisher>()
                .InSingletonScope();
            //Consumer
            KernelInstance.Bind<IConsumerEngine>()
                .To<ConsumerEngine>()
                .InSingletonScope();
            KernelInstance.Bind<IConsumerServiceReceiver>()
                .To<ConsumerServiceReceiver>()
                .InSingletonScope();
        }
    }
}
