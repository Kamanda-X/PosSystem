using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Api.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        [ForeignKey("Loyalty")]
        public Guid LoyaltyId { get; set; }

        public virtual Loyalty Loyalty { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string Email { get; set; } = null!;

        [Phone]
        [RegularExpression(@"^((8|\+370)\d{8})$")]
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
