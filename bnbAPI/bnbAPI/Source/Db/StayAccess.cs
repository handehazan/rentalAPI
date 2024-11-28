using bnbAPI.Context;
using bnbAPI.model;
using Microsoft.EntityFrameworkCore;

namespace bnbAPI.Source.Db
{
    public class StayAccess
    {
        private ApplicationDbContext _context;

        public StayAccess(ApplicationDbContext context)
        {
            _context = context;

        }

        public bool IsAvailable(int listingId, DateTime startDate, DateTime endDate)
        {
            return !_context.Stays.Any(s => s.ListingId == listingId && s.StartDate < endDate && s.EndDate > startDate);
        }

        public void AddStay(Stay stay)
        {
            _context.Stays.Add(stay);
            _context.SaveChanges();
        }

        public Stay GetStayById(int stayId)
        {
            return _context.Stays.FirstOrDefault(s => s.Id == stayId);
        }

        public List<Stay> GetStaysForListing(int listingId)
        {
            return _context.Stays.Where(s => s.ListingId == listingId).ToList();
        }

        public bool HasStay(int stayId)
        {
            return _context.Stays.Any(s => s.Id == stayId);
        }

        public bool UserHasStay(int stayId, int userId)
        {
            return _context.Stays.Any(s => s.Id == stayId && s.UserId == userId);
        }

        public List<Stay> GetStaysByUserId(int userId)
        {
            return _context.Stays.Include(s=> s.Listing).Where(s=> s.UserId == userId).ToList();

        }


    }
}
