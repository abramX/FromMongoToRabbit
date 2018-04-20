using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FromMongoToRabbit;
using RabbitMQ.Client;

namespace AppManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IDbRepository>().To<MongoRepository>();

            var mongodb = kernel.Get<IDbRepository>();
            /*mongodb.Add<Product>(new Product {
                Name = "Prodotto1",
                Description = "ProvaDescrizione bla bla vla",
                Price = 300.00,
                PriceList = new Catalog
                {
                    Name = "Catalogo1",
                    Description = "Descrizione Catalogo",
                    ExpirationDate = new DateTime(2019, 5, 23)
                }
            });*/
            foreach(Product product in mongodb.All<Product>().ToList())
            {
                Console.WriteLine(product.Name+" "+product.Description + " appartiene al catalogo:"+ product.PriceList.Name);
            }

            IList<Product> productList = mongodb.All<Product>().ToList();


            var factory = new ConnectionFactory() { HostName = "localhost", UserName="guest", Password="guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "FirstQueueIbra",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = productList.First().Description.ToString();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "IbraExchange",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            
        }
    }
}
