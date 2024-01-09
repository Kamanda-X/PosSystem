using System.ComponentModel.DataAnnotations;

namespace POS.Api.Models.DTOs
{
    public class UpdateEmployeeDto
    {
        public string UserName { get; set; } = null!;

        public required Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string Email { get; set; } = null!;

        [Phone]
        [RegularExpression(@"^((8|\+370)\d{8})$")]
        public string PhoneNumber { get; set; } = null!;
    }
}
