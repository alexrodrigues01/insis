using Votes.Helpers;
using Votes.Interfaces.Repositories;
using Votes.Model;

namespace Votes.Repositories
{
    public class ReviewRepository : IReviewRepository
    {

        private DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public bool FindReview(long reviewId)
        {
            bool exist = false;
            if(_context.Reviews.Where(x => x.reviewId == reviewId).Count() >0)
            {
                exist = true;
            }

            return exist;
        }
    }
}
