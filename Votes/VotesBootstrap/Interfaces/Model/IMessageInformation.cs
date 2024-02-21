using VotesBootstrap.DTOs;

namespace VotesBootstrap.Interfaces.Model
{
    public interface IMessageInformation
    {        
        public string typeOfEvent { get; set; }

        public string domain { get; set; }

        public List<VoteReviewDTO> entity { get; set; }
    }
}
