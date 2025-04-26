namespace Domain;

public interface IMessageService
{
    Task SaveMessageAsync(string message);
}