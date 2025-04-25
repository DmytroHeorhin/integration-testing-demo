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

        public async Task SaveDemoMessageAsync(string message)
        {
            var apiResponse = await _remoteApiService.FetchDataAsync(message);
            await _repository.SaveDataAsync(apiResponse);
            await _messageProducer.ProduceAsync(apiResponse);
        }
    }
}