using Domain.Kafka;

namespace IntegrationTests.Infrastructure;

public class FakeNoteProducer : INoteProducer
{
    private readonly List<Note> _messages = [];

    public IReadOnlyList<Note> Messages => _messages;

    public Task ProduceAsync(Note note)
    {
        _messages.Add(note);
        return Task.CompletedTask;
    }

    public void CleanProducedMessages()
    {
        _messages.Clear();
    }
}
