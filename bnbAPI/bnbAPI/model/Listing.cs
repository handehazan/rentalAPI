using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bnbAPI.model
{
    [Table("Listing")]

    public class Listing
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } =string.Empty;

        [Required]
        public int MaxPeople { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; } = 0;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TotalNumberOfRatings { get; set; } = 0; // Tracks the count of ratings
        public decimal TotalRatingSum { get; set; } = 0;   // Tracks the sum of all ratings

        // Navigation Property to Stays
        public List<Stay> Stays { get; set; } = new List<Stay>(); // A listing can have many stays

    }
}
