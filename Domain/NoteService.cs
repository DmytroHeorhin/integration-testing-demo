using Domain.Kafka;
using Domain.Remote;
using Domain.Repositories;
using Domain.UserContext;

namespace Domain;

public class NoteService : INoteService
{
    private readonly IRemoteApiClient _remoteApiService;
    private readonly INoteProducer _noteProducer;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;

    public NoteService(IRemoteApiClient remoteApiService, INoteProducer noteProducer, IUserContext userContext, IUserRepository userRepository)
    {
        _remoteApiService = remoteApiService;
        _noteProducer = noteProducer;
        _userContext = userContext;
        _userRepository = userRepository;
    }

    public async Task SaveNoteAsync(string note)
    {
        var userEmail = _userContext.GetUserEmail();
        var userId = await _userRepository.GetUserIdByEmailAsync(userEmail);
        var score = await _remoteApiService.GetScoreAsync(note);

        var noteObject = new Note
        {
            Text = note,
            UserId = userId,
            Score = score
        };

        await _noteProducer.ProduceAsync(noteObject);
    }
}
