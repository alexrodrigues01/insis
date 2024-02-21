using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using VotesBootstrap.DTOs;
using VotesBootstrap.Helpers;
using VotesBootstrap.Interfaces.Model;
using static VotesBootstrap.Interfaces.Model.IMessageInformation;

namespace VotesBootstrap.Model
{
    public class MessageInformation: IMessageInformation
    {
        [BsonElement("TypeOfEvent")]
        public string typeOfEvent { get; set; }

        [BsonElement("Domain")]
        public string domain { get; set; }

        [BsonElement("Entity")]
        public List<VoteReviewDTO> entity { get; set; }

        public MessageInformation(string typeOfEvent, string domain, VoteReviewDTO entity)
        {
            this.typeOfEvent = typeOfEvent;
            this.domain = domain;
            this.entity = new List<VoteReviewDTO> { entity };
        }
        public MessageInformation(string typeOfEvent, string domain, List<VoteReviewDTO> entity)
        {
            this.typeOfEvent = typeOfEvent;
            this.domain = domain;
            this.entity = entity;
        }

        public MessageInformation() { }
    }
}
