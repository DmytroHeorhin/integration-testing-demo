using Domain.Kafka;
using Domain.Remote;
using Domain.Repositories;
using Domain.UserContext;

namespace Domain
{
    public class MessageService : IMessageService
    {
        private readonly IRemoteApiClient _remoteApiService;
        private readonly IMessageRepository _repository;
        private readonly IKafkaProducer _messageProducer;
        private readonly IUserContext _userContext;

        public MessageService(IRemoteApiClient remoteApiService, IMessageRepository repository, IKafkaProducer messageProducer, IUserContext userContext)
        {
            _remoteApiService = remoteApiService;
            _repository = repository;
            _messageProducer = messageProducer;
            _userContext = userContext;
        }

        public async Task SaveMessageAsync(string message)
        {
            var userEmail = _userContext.GetUserEmail();
            var messageOccurrenceCount = await _remoteApiService.GetOccurrenceCountAsync(message);
            await _repository.SaveMessageAsync(message, messageOccurrenceCount, userEmail);
            await _messageProducer.ProduceAsync(message);
        }
    }
}