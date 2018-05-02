using System.Configuration;
using FromMongoToRabbit;
using Ninject.Modules;

namespace AppManager
{
    public class IocInstaller : NinjectModule
    {
        public override void Load()
        {


            /*KernelInstance.Bind<MongoDbClient>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoConnectionSender"].ConnectionString);
         */
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

//            KernelInstance.Bind<MongoDbClientReceiver>()
//                .ToSelf()
//                .InSingletonScope()
//                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoConnectionReceiver"].ConnectionString);

            Bind<IProductDbReceiver>()
               .To<ProductDbReceiver>()
               .InSingletonScope();
            //Producer
            Bind<IEngine>()
                .To<Engine>()
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
