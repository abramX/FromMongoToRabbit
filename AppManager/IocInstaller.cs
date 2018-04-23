using Ninject.Modules;
using FromMongoToRabbit;
using System;

namespace AppManager
{
    public class IocInstaller : NinjectModule
    {
        public override void Load()
        {
            KernelInstance.Bind<IDbRepository>().To<MongoRepository>().InSingletonScope()
                   .WithConstructorArgument("connectionString", "mongodb://localhost:27017")
                   .WithConstructorArgument("dbName", "mydb")
                   .WithConstructorArgument("dbCollection", "products");
            KernelInstance.Bind<IEngine>().To<Engine>().InSingletonScope();
            KernelInstance.Bind<IServicePublisher>().To<ServicePublisher>().InSingletonScope();
        }
    }
}
