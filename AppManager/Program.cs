using Ninject;
using System;
using System.Reflection;
using System.Threading.Tasks;
using FromMongoToRabbit;
using Ninject.Modules;
using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;
using System.Configuration;


namespace AppManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var cronPublicationExpression = ConfigurationManager.AppSettings["cronPublicationExpression"];
            Console.WriteLine(cronPublicationExpression);
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

                    s.UseQuartzNinject();
                    s.ScheduleQuartzJob(feed => feed.WithJob(JobBuilder.Create<SendMesssageJob>().Build)
                            .AddTrigger(() => TriggerBuilder.Create()
                                .WithCronSchedule(cronPublicationExpression)
                                .WithIdentity("SendMesssage")
                                .Build()));

                });
                a.StartAutomatically();
                a.RunAsLocalService();
                a.EnableShutdown();
            }).Run();

        }
    }
}

