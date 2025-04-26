namespace Domain.Kafka
{
    public interface IKafkaProducer
    {
        Task ProduceAsync(string value);
    }
}