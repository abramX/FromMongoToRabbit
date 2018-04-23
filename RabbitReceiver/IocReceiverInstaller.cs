using Ninject.Modules;
using FromMongoToRabbit;
using System;

namespace RabbitReceiver
{
    public class IocReceiverInstaller : NinjectModule
    {
        public override void Load()
        {
            KernelInstance.Bind<IDbRepository>().To<MongoRepository>().InSingletonScope()
                   .WithConstructorArgument("connectionString", "mongodb://localhost:27017")
                   .WithConstructorArgument("dbName", "mydbreceived")
                   .WithConstructorArgument("dbCollection", "products_received");
            KernelInstance.Bind<IConsumerEngine>().To<ConsumerEngine>().InSingletonScope();
            KernelInstance.Bind<IConsumerServicePublisher>().To<ConsumerServicePublisher>().InSingletonScope();
        }
    }
}
