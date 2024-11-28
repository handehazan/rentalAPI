using System.ComponentModel.DataAnnotations;

namespace bnbAPI.model.Dto
{
    public class StayDto
    {
        public int ListingId { get; set; }
        public string GuestNames { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class ShowStayDto
    {
        public int StayId { get; set; }
        public int ListingId { get; set; }

        public int UserId { get; set; }
        public string GuestNames { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
