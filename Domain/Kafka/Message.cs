namespace Domain.Kafka;

public class Message
{
    public int UserId { get; set; }
    public string MessageText { get; set; } = string.Empty;
    public int MessageOccurrenceCount { get; set; }
}