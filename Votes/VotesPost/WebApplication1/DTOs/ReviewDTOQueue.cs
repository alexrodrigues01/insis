namespace Votes.DTOs
{
    public class ReviewDTOQueue
    {

        public string typeOfEvent { get; set;}

        public ReviewRabbitEntity entity { get; set;}

        public string domain { get; set;}

        public ReviewDTOQueue(string typeOfEvent, string domain, ReviewRabbitEntity entity)
        {
            this.typeOfEvent = typeOfEvent;
            this.domain = domain;
            this.entity = entity;
        }
    }
}
