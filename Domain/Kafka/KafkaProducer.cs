using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Domain.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public KafkaProducer(IProducer<string, string> producer, IConfiguration configuration)
        {
            _producer = producer;
            _topic = configuration["Kafka:Topic"];
        }

        public async Task ProduceAsync(string value)
        {
            await _producer.ProduceAsync(_topic, new Message<string, string> { Key = null, Value = value });
        }
    }
}