using VotesApp.DTOs;
using VotesApp.Model;

namespace VotesApp.Interfaces.Repository;

public interface IVoteRepository
{
    public Task<Boolean> FindVote(VoteReviewDTO vote);

    public Task CreateVote(Vote vote);
}