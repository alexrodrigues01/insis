using Newtonsoft.Json;
using static VotesBootstrap.Interfaces.Model.IMessageInformation;

namespace VotesBootstrap.DTOs
{
    public class MessageInformationDTO
    {
        public TypeOfEvent typeOfEvent { get; set; }

        public string domain { get; set; }

        public Object entity { get; set; }

        public MessageInformationDTO(TypeOfEvent typeOfEvent, string domain, Object entity)
        {
            this.typeOfEvent = typeOfEvent;
            this.domain = domain;
            this.entity = entity;
        }

        public enum TypeOfEvent
        {
            CREATE,
            GET
        }
    }
}
