using AutoMapper;
using VotesBootstrap.Interfaces.Model;
using VotesBootstrap.DTOs;

namespace VotesBootstrap.Helpers
{
    public class VoteProfile : Profile
    {

        public VoteProfile()
        {
            CreateMap<IVote, VoteReviewDTO>()
                .ForMember(voteDTO => voteDTO.userId, vote => vote.MapFrom(vote => vote.userId))
                .ForMember(voteDTO => voteDTO.reviewId, vote => vote.MapFrom(vote => vote.reviewId))
                .ForMember(voteDTO => voteDTO.vote, vote => vote.MapFrom(vote => vote.vote));
        }
    }
}
