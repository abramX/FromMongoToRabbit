using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using log4net;
using Polly;
using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;
using Topshelf.WebApi;
using Topshelf.WebApi.Ninject;

namespace AppManager
{
    internal static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            var policy = Policy.Handle<Exception>().Retry(10);
            policy.Execute(MainApp);
        }

        private static void MainApp()
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