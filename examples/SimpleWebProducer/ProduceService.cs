using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RdKafka;

namespace SimpleWebProducer
{
    public class ProduceService : IProduceService
    {

        private Producer _producer;
        private Topic _topic;

        public ProduceService()
        {
            string brokerList = "kafka:9092";
            string topicName = "Benchmarking.Web";

            _producer = new Producer(brokerList);
            _topic = _producer.Topic(topicName);
        }

        public void Produce()
        {
            System.Diagnostics.Debug.WriteLine($"{_producer.Name} producing on {_topic.Name}.");

            byte[] data = Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString());
            Task<DeliveryReport> deliveryReport = _topic.Produce(data);
            var unused = deliveryReport.ContinueWith(task =>
            {
                System.Diagnostics.Debug.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
            });
        }

        public void Dispose()
        {
            _topic.Dispose();
            _producer.Dispose();
        }
    }
}
