using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Kafka;

namespace IntegrationTests.Infrastructure
{
    public class FakeMessageProducer : IMessageProducer
    {
        private readonly List<Message> _messages = [];

        public IReadOnlyList<Message> Messages => _messages;

        public Task ProduceAsync(Message message)
        {
            _messages.Add(message);
            return Task.CompletedTask;
        }

        public void CleanProducedEvents()
        {
            _messages.Clear();
        }
    }
}