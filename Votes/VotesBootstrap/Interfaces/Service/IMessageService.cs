using VotesBootstrap.Model;

namespace VotesBootstrap.Interfaces.Service
{
    public interface IMessageService
    {
        public void CreateMessage(MessageInformation message);
        public void CreateListMessage(List<MessageInformation> message);
    }
}
