using bnbAPI.model;

namespace bnbAPI.Source.Svc
{
    public interface IStayService
    {
        public Stay GetStayById(int stayId);
        public List<Stay> GetStaysForListing(int listingId);
        public void BookStay(Stay stay);
        public List<Stay> GetStaysByUserId(int userId);

    }
}
