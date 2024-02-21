using MongoDB.Driver;
using VotesBootstrap.Interfaces.Repository;
using VotesBootstrap.Interfaces.Service;
using VotesBootstrap.Model;
namespace VotesBootstrap.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _repository;

    public MessageService(IMessageRepository repository)
    {
        _repository = repository;
    }

    public async void CreateMessage(MessageInformation message)
    {
        try
        {
            await _repository.CreateMessage(message);
        }
        catch 
        {

            throw new Exception();

        }
    }

    public async void CreateListMessage(List<MessageInformation> message)
    {
        try
        {
            await _repository.CreateListMessage(message);
        }
        catch
        {

            throw new Exception();

        }
    }
}