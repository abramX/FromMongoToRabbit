using NUnit.Framework;
using RabbitSender;
using Ninject;
using FromMongoToRabbit;

namespace RabbitSenderTest
{
    [TestFixture]
    public class IocTest
    {
        [Test]
        public void TestIoc()
        {
            var kernel = new StandardKernel( new Ninject.Modules.INinjectModule[] { new IocInstaller() });
            Assert.AreNotEqual(null, kernel.Get<MongoDbClient>());
            Assert.AreNotEqual(null, kernel.Get<IProductDbSender>());
            Assert.AreNotEqual(null, kernel.Get<MongoDbClientReceiver>());
            Assert.AreNotEqual(null, kernel.Get<IProductDbReceiver>());
            Assert.AreNotEqual(null, kernel.Get<IEngine>());
            Assert.AreNotEqual(null, kernel.Get<IConsumerEngine>());
            Assert.AreNotEqual(null, kernel.Get<IConsumerServiceReceiver>());
            Assert.AreNotEqual(null, kernel.Get<IServicePublisher>());
        }

    }
}
