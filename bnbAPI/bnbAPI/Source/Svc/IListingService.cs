using bnbAPI.model;

namespace bnbAPI.Source.Svc
{
    public interface IListingService
    {
        List<Listing> GetAllListings();
        Listing GetListingById(int ListingId);
        void AddListing(Listing listing);
        //void DeleteListing(int ListingId);
        decimal GetAverageRating(int ListingId);
        public List<Listing> QueryListings(string country, string city, int? maxPeople, DateTime? from, DateTime? to);
        List<Listing> ReportListings(string country, string? city, decimal? minRating);

    }
}
