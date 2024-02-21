using Microsoft.AspNetCore.Mvc;
using VotesGet.DTOs;

namespace VotesGet.Interfaces.Services;

public interface IVoteService
{
    public Task<List<VoteReviewDTO>> GetVoteByReview(long reviewID);
    //public Task<VoteReviewDTO> CreateVoteByRabbit(VoteReviewDTO voteDTO);
    public bool SendMessageInformation(List<VoteReviewDTO> voteReviewDTO);
}