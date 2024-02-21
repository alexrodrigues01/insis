using VotesApp.DTOs;

namespace Votes.DTOs
{
    public class MessageInformationDTO
    {
        public TypeOfEvent typeOfEvent { get; set; }

        public string domain { get; set; }

        public VoteReviewDTO entity { get; set; }

        public MessageInformationDTO(TypeOfEvent typeOfEvent, string domain, VoteReviewDTO entity)
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
