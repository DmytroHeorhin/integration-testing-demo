using Domain.Kafka;
using Domain.Remote;
using Domain.Repositories;
using Domain.UserContext;

namespace Domain
{
    public class MessageService : IMessageService
    {
        private readonly IRemoteApiClient _remoteApiService;
        private readonly IMessageProducer _messageProducer;
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;

        public MessageService(IRemoteApiClient remoteApiService, IMessageProducer messageProducer, IUserContext userContext, IUserRepository userRepository)
        {
            _remoteApiService = remoteApiService;
            _messageProducer = messageProducer;
            _userContext = userContext;
            _userRepository = userRepository;
        }

        public async Task SaveMessageAsync(string message)
        {
            var userEmail = _userContext.GetUserEmail();
            var userId = await _userRepository.GetUserIdByEmailAsync(userEmail);
            var messageOccurrenceCount = await _remoteApiService.GetOccurrenceCountAsync(message);

            var messageObject = new Message
            {
                MessageText = message,
                UserId = userId,
                MessageOccurrenceCount = messageOccurrenceCount
            };

            await _messageProducer.ProduceAsync(messageObject);
        }
    }
}