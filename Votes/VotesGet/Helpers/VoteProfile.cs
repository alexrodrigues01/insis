using AutoMapper;
using VotesGet.Interfaces.Model;
using VotesGet.DTOs;

namespace VotesGet.Helpers
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
