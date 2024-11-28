using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bnbAPI.model
{
    [Table("Stays")]
    public class Stay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ListingId { get; set; }  // Foreign Key to Listing
        public Listing Listing { get; set; } // Navigation Property to Listing
        public int UserId { get; set; }  // Foreign Key to User
        public User User { get; set; }  // Navigation Property to User

        [Required]
        public string GuestNames { get; set; } = string.Empty;  // Names of guests stored as a single string

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;  
    }
}
