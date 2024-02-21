using Votes.Interfaces.Model;

namespace Votes.Model
{
    public class Review : IReview 
    {

        public long reviewId { get; set; }

        public Review(long reviewId)
        {
            this.reviewId = reviewId;
        }

        protected bool Equals(Review other)
        {
            return reviewId == other.reviewId;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Review)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(reviewId);
        }
    }
}
