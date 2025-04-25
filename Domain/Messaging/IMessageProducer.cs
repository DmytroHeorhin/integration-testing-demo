namespace Domain.Messaging
{
    public interface IMessageProducer
    {
        Task ProduceAsync(string value);
    }
}