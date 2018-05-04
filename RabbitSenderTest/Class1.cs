using FromMongoToRabbit.Engine.Consumer;
using FromMongoToRabbit.Engine.Producer;
using NUnit.Framework;
using Ninject;
using FromMongoToRabbit.MongoDB;
using FromMongoToRabbit.Service.Ioc;

namespace RabbitSenderTest
{
    [TestFixture]
    public class IocTest
    {
        [Test]
        public void TestIoc()
        {
            var kernel = new StandardKernel( new Ninject.Modules.INinjectModule[] { new IocInstaller() });
            //Assert.IsNotNull(kernel.Get<IMongoDbClient>("SenderDb"));
            //Assert.IsNotNull(kernel.Get<IMongoDbClient>("ReceiverDb"));
            Assert.IsNotNull(kernel.Get<IProductDbSender>());           
            Assert.IsNotNull(kernel.Get<IProductDbReceiver>());
            Assert.IsNotNull(kernel.Get<IEngine>());
            Assert.IsNotNull(kernel.Get<IConsumerEngine>());
            Assert.IsNotNull(kernel.Get<IConsumerServiceReceiver>());
            Assert.IsNotNull(kernel.Get<IServicePublisher>());
        }

    }
}
