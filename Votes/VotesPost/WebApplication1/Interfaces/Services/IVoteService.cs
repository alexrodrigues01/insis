using Microsoft.AspNetCore.Mvc;
using VotesApp.DTOs;

namespace VotesApp.Interfaces.Services;

public interface IVoteService
{
    public Task<VoteReviewDTO> CreateVote(VoteReviewDTO voteDTO);
    public bool SendVote(VoteReviewDTO voteDTO);
    public bool SendMessageInformation(VoteReviewDTO voteDTO);
}