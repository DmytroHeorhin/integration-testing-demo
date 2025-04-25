using Domain.Messaging;
using Domain.Remote;
using Domain.Repositories;

namespace Domain
{
    public class DemoService : IDemoService
    {
        private readonly IDemoRepository _repository;
        private readonly IRemoteApiService _remoteApiService;
        private readonly IMessageProducer _messageProducer;

        public DemoService(IDemoRepository repository, IRemoteApiService remoteApiService, IMessageProducer messageProducer)
        {
            _repository = repository;
            _remoteApiService = remoteApiService;
            _messageProducer = messageProducer;
        }

        public async Task<string> GetDemoMessageAsync()
        {
            var localData = _repository.GetData();
            var remoteData = await _remoteApiService.FetchDataAsync();
            var message = $"Local: {localData}, Remote: {remoteData}";

            await _messageProducer.ProduceAsync(message);

            return message;
        }
    }
}