using System.ComponentModel.DataAnnotations;

namespace bnbAPI.model.Dto
{
    public class ListingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int MaxPeople { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalNumberOfRatings { get; set; }
        public decimal TotalRatingSum { get; set; }
    }

    public class InsertListingDto
    {

        public string Title { get; set; }=string.Empty;
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int MaxPeople { get; set; }
        [Required]
        public decimal Price { get; set; }

    }

    public class ShowListingDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int MaxPeople { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }

    }


}
