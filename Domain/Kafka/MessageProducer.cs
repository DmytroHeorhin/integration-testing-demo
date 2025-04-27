using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MessageDto = Domain.Kafka.Message;

namespace Domain.Kafka;

public class MessageProducer : IMessageProducer
{
    private readonly IProducer<string, MessageDto> _producer;
    private readonly string _topic;

    public MessageProducer(IProducer<string, MessageDto> producer, IOptions<KafkaOptions> options)
    {
        _producer = producer;
        _topic = options.Value.MessageTopic;
    }

    public async Task ProduceAsync(MessageDto message)
    {
        await _producer.ProduceAsync(_topic, new Message<string, MessageDto>
        {
            Key = null!,
            Value = message
        });
    }
}