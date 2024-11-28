using bnbAPI.Context;
using bnbAPI.model;
using bnbAPI.model.Dto;
using bnbAPI.Source.Db;

namespace bnbAPI.Source.Svc
{
    public class ListingService : IListingService
    {
        private ApplicationDbContext _context;

        public ListingService(ApplicationDbContext context)
        {
            _context = context;

        }

        public Listing GetListingById(int ListingId)
        {
            ListingAccess access = new ListingAccess(_context);
            return access.GetListingById(ListingId);
        }

        public List<Listing> GetAllListings()
        {
            ListingAccess access = new ListingAccess(_context);
            return access.GetAllListings();
        }

        public void AddListing(Listing listing)
        {
            ListingAccess access = new ListingAccess(_context);

            if (listing == null)
            {
                throw new ArgumentNullException(nameof(listing), "Listing cannot be null.");
            }
            access.insertListing(listing);
        }

        public List<Listing> QueryListings(string country, string city, int? maxPeople, DateTime? startDate, DateTime? endDate)
        {
            ListingAccess access = new ListingAccess(_context);
            return access.GetListingWithQuery(country, city, maxPeople, startDate, endDate);
        }

        public decimal GetAverageRating(int ListingId)
        {
            ListingAccess access = new ListingAccess(_context);

            decimal averageRating = access.getListingRating(ListingId);
            return averageRating;
        }

        public List<Listing> ReportListings(string country, string? city, decimal? minRating)
        {
            ListingAccess access = new ListingAccess(_context);
            return access.GetFilteredListings(country, city, minRating);
        }


        /*public void DeleteListing(int ListingId)
        {
            ListingAccess access = new ListingAccess(_context);
            access.deleteListing(ListingId);
        }*/



    }
}
