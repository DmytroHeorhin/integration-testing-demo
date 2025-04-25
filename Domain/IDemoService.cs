namespace Domain;

public interface IDemoService
{
    Task<string> GetDemoMessageAsync();
}