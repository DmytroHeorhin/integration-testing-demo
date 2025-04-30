namespace Domain.Kafka;

public interface INoteProducer
{
    Task ProduceAsync(Note note);
} 