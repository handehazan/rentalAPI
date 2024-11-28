using bnbAPI.Context;
using bnbAPI.model;
using bnbAPI.Source.Db;

namespace bnbAPI.Source.Svc
{
    public class StayService : IStayService
    {
        private ApplicationDbContext _context;

        public StayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Stay GetStayById(int stayId)
        {
            StayAccess access = new StayAccess(_context);
            return access.GetStayById(stayId);
        }

        public List<Stay> GetStaysForListing(int listingId)
        {
            StayAccess access = new StayAccess(_context);
            return access.GetStaysForListing(listingId);

        }

        public void BookStay(Stay stay)
        {
            ListingAccess listingAccess = new ListingAccess(_context);
            StayAccess stayAccess = new StayAccess(_context);

            var listing = listingAccess.GetListingById(stay.ListingId);

            bool isAvailable = stayAccess.IsAvailable(stay.ListingId, stay.StartDate, stay.EndDate);
            if (!isAvailable)
            {
                throw new InvalidOperationException("The listing is not available for the selected dates.");
            }

            if (stay == null)
            {
                throw new ArgumentException("Stay cannot be null.");
            }

            if (stay.UserId < 0)
            {
                throw new ArgumentException("User Id not found.");
            }

            if (listing == null)
            {
                throw new ArgumentException("Listing not found");
            }

            if (stay.StartDate >= stay.EndDate)
            {
                throw new ArgumentException("Invalid dates. Start date must be before end date.");
            }

            stayAccess.AddStay(stay);

        }

        public List<Stay> GetStaysByUserId(int userId)
        {
            StayAccess access = new StayAccess(_context);
            return access.GetStaysByUserId(userId);
        }

    }
}
