namespace Domain.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IDbClient _dbClient;

        public MessageRepository(IDbClient dbClient)
        {
            _dbClient = dbClient ?? throw new ArgumentNullException(nameof(dbClient));
        }

        public Task SaveMessageAsync(string message, int occurrenceCount, string authorEmail)
        {
            var query = "INSERT INTO messages (Message, OccurrenceCount, AuthorEmail) " +
                        "VALUES (@Message, @OccurrenceCount, @AuthorEmail)";
            return _dbClient.ExecuteAsync(query, new { Message = message, OccurrenceCount = occurrenceCount, AuthorEmail = authorEmail });
        }
    }
}