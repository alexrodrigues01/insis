using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotesGet.DTOs;

public class VoteReviewDTO
{
    [ForeignKey("userId")]
    public long userId { get; set; }

    [Required]
    public string vote { get; set; }

    [ForeignKey("reviewId")]
    public long reviewId { get; set; }

    public VoteReviewDTO(long userId, string vote, long reviewId)
    {
        this.userId = userId;
        this.vote = vote;
        this.reviewId = reviewId;
    }

    public VoteReviewDTO() {}
}