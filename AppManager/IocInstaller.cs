using Ninject.Modules;
using FromMongoToRabbit;
using System.Configuration;

namespace RabbitSender
{
    public class IocInstaller : NinjectModule
    {
        public override void Load()
        {
            KernelInstance.Bind<IDbRepository>().To<MongoRepository>().InSingletonScope()
                   .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString)
                   .WithConstructorArgument("dbName", ConfigurationManager.AppSettings["MongoDbName"])
                   .WithConstructorArgument("dbCollection", ConfigurationManager.AppSettings["MongoDbCollection"]);
            KernelInstance.Bind<IEngine>().To<Engine>().InSingletonScope();
            KernelInstance.Bind<IServicePublisher>().To<ServicePublisher>().InSingletonScope();
        }
    }
}
