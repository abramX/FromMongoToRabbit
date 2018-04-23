using Ninject;
using System;
using System.Reflection;
using System.Threading.Tasks;
using FromMongoToRabbit;
using Ninject.Modules;
using Topshelf;
using Topshelf.Ninject;

namespace AppManager
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.New(a =>
            {
                a.SetServiceName("AppManager");
                a.UseNinject(new IocInstaller());
                a.Service<AppMangerService>(s =>
                {
                    s.ConstructUsingNinject();
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service =>
                    {
                        if (NinjectBuilderConfigurator.Kernel != null)
                            NinjectBuilderConfigurator.Kernel.Dispose();
                    });

                });
                a.StartAutomatically();
                a.RunAsLocalService();
                a.EnableShutdown();
            }).Run();

            

        }
    }
}
