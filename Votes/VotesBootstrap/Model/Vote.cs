using System.ComponentModel.DataAnnotations;
using VotesBootstrap.Interfaces.Model;

namespace VotesBootstrap.Model;

public class Vote : IVote
{
    [Required]
    public string vote { get; set; }

    [Required]
    public long userId { get; set; }

    [Required]
    public long reviewId { get; set; }

    public Vote(string vote, long userId, long reviewId)
    {
        this.vote = vote;
        this.userId = userId;
        this.reviewId = reviewId;
    }

    protected bool Equals(Vote other)
    {
        return vote == other.vote && userId == other.userId && reviewId == other.reviewId;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Vote)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(vote, userId, reviewId);
    }
    
    
}