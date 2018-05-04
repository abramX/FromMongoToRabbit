using System.Configuration;
using FromMongoToRabbit.Engine.Consumer;
using FromMongoToRabbit.Engine.Producer;
using FromMongoToRabbit.MongoDB;
using Ninject.Modules;

namespace FromMongoToRabbit.Service.Ioc
{
    public class IocInstaller : NinjectModule
    {
        public override void Load()
        {

            Bind<IMongoDbClient>()
                .To<MongoDbClient>()
                .WhenInjectedInto(typeof(ProductDbSender))
                .InSingletonScope()
                .Named("SenderDb")
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoConnectionSender"].ConnectionString);

            Bind<IMongoDbClient>()
                .To<MongoDbClient>()
                .WhenInjectedInto(typeof(ProductDbReceiver))
                .InSingletonScope()
                .Named("ReceiverDb")
                
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoConnectionReceiver"].ConnectionString);


            Bind<IProductDbSender>()
               .To<ProductDbSender>()
               .InSingletonScope();

            Bind<IProductDbReceiver>()
               .To<ProductDbReceiver>()
               .InSingletonScope();
            //Producer
            Bind<IEngine>()
                .To<Engine.Producer.Engine>()
                .InSingletonScope();

            Bind<IServicePublisher>()
                .To<ServicePublisher>()
                .InSingletonScope();
            //Consumer
            Bind<IConsumerEngine>()
                .To<ConsumerEngine>()
                .InSingletonScope();
            Bind<IConsumerServiceReceiver>()
                .To<ConsumerServiceReceiver>()
                .InSingletonScope();
        }
    }
}
