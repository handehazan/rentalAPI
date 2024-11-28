using System.ComponentModel.DataAnnotations;

namespace bnbAPI.model.Dto
{
    public class ReviewDto
    {
        [Required]
        public int StayId { get; set; }
        [Required]
        public int Rating { get; set; } 
        [Required]
        public string Comment { get; set; } 
    }
}
