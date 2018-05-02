﻿using FromMongoToRabbit;
using System.Web.Http;
using log4net;

namespace AppManager
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private IProductDbSender _mongo;
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public ProductController(IProductDbSender mongo)
        {
            _mongo = mongo;
        }

        [Route("fill")]
        [HttpGet]
        public string FillProduct()
        {
            _log.Info("Filling the sender db");
            _mongo.FillMongoDb();
            
            return ("Filling Products....");
        }
        

    }


}
