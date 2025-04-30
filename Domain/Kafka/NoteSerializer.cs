using Confluent.Kafka;
using System.Text.Json;

namespace Domain.Kafka
{
    public class NoteSerializer : ISerializer<Note>
    {
        public byte[] Serialize(Note data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }
    }
} 