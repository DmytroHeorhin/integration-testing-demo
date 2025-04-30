namespace Domain.Kafka;

public class Note
{
    public int UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Score { get; set; }
} 