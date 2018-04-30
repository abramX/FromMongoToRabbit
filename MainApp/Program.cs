using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using RabbitSender;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;


namespace MainApp
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
                    s.ScheduleQuartzJob(feed => feed.WithJob(JobBuilder.Create<MessageSendJob>().Build)
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
