using VotesGet.DTOs;
using VotesGet.Model;

namespace VotesGet.Interfaces.Repository;

public interface IVoteRepository
{
    public Task<List<VoteReviewDTO>> GetVoteByReview(long reviewID);

    public Task CreateVote(Vote vote);
}