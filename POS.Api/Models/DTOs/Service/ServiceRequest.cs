using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS.Api.Models.DTOs.Service
{
    public class ServiceRequest
    {
        public Guid? Id { get; init; }

        public required string EmployeeId { get; set; } = null!;

        [MaxLength(100)]
        public required string Name { get; set; } = null!;

        [MaxLength(512)]
        public string Description { get; set; } = string.Empty;

        public float Price { get; set; }
    }
}
