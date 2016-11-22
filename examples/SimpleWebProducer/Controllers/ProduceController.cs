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
        [HttpGet]
        public IActionResult Produce()
        {
            string brokerList = "kafka:9092";
            string topicName = "Benchmarking.Web";

            using (Producer producer = new Producer(brokerList))
            using (Topic topic = producer.Topic(topicName))
            {
                System.Diagnostics.Debug.WriteLine($"{producer.Name} producing on {topic.Name}.");

                byte[] data = Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString());
                Task<DeliveryReport> deliveryReport = topic.Produce(data);
                var unused = deliveryReport.ContinueWith(task =>
                {
                    System.Diagnostics.Debug.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
                });
            }

            return new OkResult();
        }
    }
}
