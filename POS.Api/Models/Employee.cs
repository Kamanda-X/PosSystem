using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace POS.Api.Models
{
    public enum Role
    {
        Admin,
        Employee
    }

    public class Employee : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public override string Email { get; set; } = null!;

        [Phone]
        [RegularExpression(@"^((8|\+370)\d{8})$")]
        public override string PhoneNumber { get; set; } = null!;

        public Role Role { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
