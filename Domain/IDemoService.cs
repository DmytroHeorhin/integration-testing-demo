namespace Domain;

public interface IDemoService
{
    Task SaveDemoMessageAsync(string message);
}