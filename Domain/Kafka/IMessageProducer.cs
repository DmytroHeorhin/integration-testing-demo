namespace Domain.Kafka;

public interface IMessageProducer
{
    Task ProduceAsync(Message message);
}