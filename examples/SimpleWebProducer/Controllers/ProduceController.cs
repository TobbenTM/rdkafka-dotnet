using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RdKafka;

namespace SimpleWebProducer.Controllers
{
    [Route("api/produce")]
    public class ProduceController
    {
        private readonly IProduceService _producer;

        public ProduceController(IProduceService producer)
        {
            _producer = producer;
        }

        [HttpGet]
        public IActionResult Produce()
        {
            _producer.Produce();

            return new OkResult();
        }
    }
}
