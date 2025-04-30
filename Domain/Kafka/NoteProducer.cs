using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Domain.Kafka;

public class NoteProducer : INoteProducer
{
    private readonly IProducer<string, Note> _producer;
    private readonly string _topic;

    public NoteProducer(IProducer<string, Note> producer, IOptions<KafkaOptions> options)
    {
        _producer = producer;
        _topic = options.Value.NoteTopic;
    }

    public async Task ProduceAsync(Note note)
    {
        await _producer.ProduceAsync(_topic, new Message<string, Note>
        {
            Key = null!,
            Value = note
        });
    }
} 