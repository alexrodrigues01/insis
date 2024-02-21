using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotesApp.DTOs;

public class VoteReviewDTO
{
    public long userId { get; set; }

    public string vote { get; set; }

    public long reviewId { get; set; }

    public VoteReviewDTO(long userId, string vote, long reviewId)
    {
        this.userId = userId;
        this.vote = vote;
        this.reviewId = reviewId;
    }
}