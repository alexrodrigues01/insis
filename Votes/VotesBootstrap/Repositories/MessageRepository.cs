using VotesBootstrap.Interfaces.Repository;
using VotesBootstrap.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace VotesBootstrap.Repositories;

public class MessageRepository : IMessageRepository
{

    private readonly IMongoCollection<MessageInformation> _context;

    public MessageRepository(IOptions<MongoDatabaseInfoSettings> messageDatabaseSettings)
    {
        var mongoClient = new MongoClient(
               messageDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
                messageDatabaseSettings.Value.DatabaseName);

        _context = mongoDatabase.GetCollection<MessageInformation>(
                messageDatabaseSettings.Value.CollectionName);

    }

    public async Task CreateMessage(MessageInformation messageInformation) =>
        await _context.InsertOneAsync(messageInformation);

    public async Task CreateListMessage(List<MessageInformation> messageInformation) =>
        await _context.InsertManyAsync(messageInformation);
}