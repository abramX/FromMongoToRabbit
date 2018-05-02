using System;
using System.Configuration;
using System.Web.Http;
using log4net;
using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;
using Topshelf.WebApi;
using Topshelf.WebApi.Ninject;

namespace AppManager
{
    static class Program
    {
        private static ILog Log =LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Sender();
        }

        private static void Sender()
        {
            Log.Info("Start log INFO...");
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

                    s.WebApiEndpoint(
                                    api =>
                                    {
                                        var x = api.OnLocalhost(8080)
                                        .UseNinjectDependencyResolver();
                                        x.ServerConfigurer = httpConfiguration => httpConfiguration.MapHttpAttributeRoutes();
                                        x.Build();
                                    }
                                );
                });


                
                a.StartAutomatically();
                a.RunAsLocalService();
                a.EnableShutdown();
            }).Run();
        }        
    }
}

