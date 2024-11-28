using bnbAPI.Context;
using bnbAPI.model;
using bnbAPI.Source.Db;

namespace bnbAPI.Source.Svc
{
    public class ReviewService : IReviewService
    {

        private ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ReviewStay(Review review,int userId)
        {
            StayAccess stayAccess = new StayAccess(_context);
            ReviewAccess reviewAccess = new ReviewAccess(_context);
            ListingAccess listingAccess = new ListingAccess(_context);

            if (review == null)
            {
                throw new ArgumentException("Review can't be null");
            }

            if (!stayAccess.UserHasStay(review.StayId,userId))
            {
                throw new UnauthorizedAccessException("You can only review stays you have booked.");
            }

            var stayExist = stayAccess.HasStay(review.StayId);

            if (!stayExist)
            {
                throw new ArgumentException("Stay not found or not booked.");
            }
            reviewAccess.AddReview(review);
            Stay s= stayAccess.GetStayById(review.StayId);
            int listingId = s.ListingId;
            listingAccess.UpdateListingRating(listingId, review.Rating);

        }
    }
}
