using bnbAPI.Context;
using bnbAPI.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace bnbAPI.Source.Db
{
    public class ListingAccess
    {
        private ApplicationDbContext _context;

        public ListingAccess(ApplicationDbContext context)
        {
            _context = context;
        }

        public Listing GetListingById(int ListingId)
        {
            return _context.Listings.FirstOrDefault(l => l.Id == ListingId);

        }

        public List<Listing> GetListingWithQuery(string country, string city, int? maxpeople, DateTime? from, DateTime? to)
        {
            var query = _context.Listings.Include(l => l.Stays).AsQueryable();

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(l => l.Country.Contains(country.ToLower()));
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(l => l.City.Contains(city.ToLower()));
            }

            if (maxpeople.HasValue)
            {
                query = query.Where(l => l.MaxPeople >= maxpeople.Value);
            }

            // Ensure the listing is available by checking the stays (booking dates)
            if (from.HasValue && to.HasValue)
            {
                query = query.Where(l => !l.Stays.Any(s => s.StartDate < to.Value && s.EndDate > from.Value));
            }

            return query.ToList();

        }

        public List<Listing> GetAllListings()
        {
            return _context.Listings.ToList();
        }

        public void insertListing(Listing listing)
        {
            _context.Listings.Add(listing);
            _context.SaveChanges();
        }

        /*public int deleteListing(int ListingId)
        {
            Listing data = GetListingById(ListingId);
            if (data != null)
            {
                _context.Listings.Remove(data);
                return _context.SaveChanges();
            }
            return 0;
        }*/

        public List<Listing> GetFilteredListings(string country, string? city, decimal? minRating)
        {
            var query = _context.Listings.AsQueryable();

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(l => l.Country.Contains(country.ToLower()));
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(l => l.City.Contains(city.ToLower()));
            }

            if (minRating.HasValue)
            {
                query = query.Where(l => l.Rating >= minRating.Value);
            }

            query = query.OrderByDescending(l => l.Rating);

            return query.ToList();
        }

        public decimal getListingRating(int ListingId)
        {
            Listing listing = GetListingById(ListingId);
            decimal rating = 0;

            if (listing != null)
            {
                rating = _context.Listings.Where(l => l.Id == ListingId).Select(l => l.Rating).FirstOrDefault();
            }
            return rating;
        }

        public void UpdateListingRating(int ListingId, decimal newRating)
        {
            Listing listing = GetListingById(ListingId);
            if (listing != null)
            {
                listing.TotalNumberOfRatings += 1;
                listing.TotalRatingSum += newRating;

                listing.Rating = listing.TotalRatingSum / listing.TotalNumberOfRatings;


                _context.SaveChanges();
            }
        }

    }
}
