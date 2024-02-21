using VotesBootstrap.DTOs;
using VotesBootstrap.Model;

namespace VotesBootstrap.Interfaces.Repository;

public interface IMessageRepository
{
    public Task CreateMessage(MessageInformation message);

    public Task CreateListMessage(List<MessageInformation> message);
}