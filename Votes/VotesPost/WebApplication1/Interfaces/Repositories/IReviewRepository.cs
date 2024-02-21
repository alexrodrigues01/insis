using Votes.Model;

namespace Votes.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        public Task CreateReview(Review review);

        public bool FindReview(long reviewId);
    }
}
