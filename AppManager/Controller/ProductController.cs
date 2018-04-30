using FromMongoToRabbit;
using System.Web.Http;

namespace AppManager
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private IProductDbSender _mongo;


        public ProductController(IProductDbSender mongo)
        {
            _mongo = mongo;
        }

        [Route("fill")]
        [HttpGet]
        public string FillProduct()
        {
            _mongo.FillMongoDb();
            return ("Filling Products....");
        }
        

    }


}
