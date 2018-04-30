using System;
using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;
using System.Configuration;
using RabbitSender;
using System.Threading.Tasks;

namespace RabbitSender
{
    public static class SenderServiceExecuter
    {
        public static void Execute()
        {
            var cronPublicationExpression = ConfigurationManager.AppSettings["cronPublicationExpression"];
            Console.WriteLine(cronPublicationExpression);
            HostFactory.New(a =>
            {
                a.SetServiceName("RabbitSender");
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
