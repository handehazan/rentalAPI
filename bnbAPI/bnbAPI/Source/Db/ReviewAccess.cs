using bnbAPI.Context;
using bnbAPI.model;

namespace bnbAPI.Source.Db
{
    public class ReviewAccess
    {
        private ApplicationDbContext _context;

        public ReviewAccess(ApplicationDbContext context)
        {
            _context = context;

        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public bool HasStay(int stayId)
        {
            return _context.Stays.Any(s => s.Id == stayId);
        }

    }
}
