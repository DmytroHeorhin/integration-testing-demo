namespace Domain;

public interface INoteService
{
    Task SaveNoteAsync(string note);
} 