using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Ninject;

namespace RabbitReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.New(a =>
            {
                a.SetServiceName("ReceiverManager");
                a.UseNinject(new IocReceiverInstaller());
                a.Service<ConsumerService>(s =>
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
