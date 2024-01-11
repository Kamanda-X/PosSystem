using POS.Api.Models.DTOs.TimeSlot;
using System.ComponentModel.DataAnnotations;

namespace POS.Api.Models.DTOs.Service
{
    public class ServiceResponse
    {
        public Guid Id { get; init; }

        public string EmployeeId { get; set; } = null!;

        public string Name { get; set; } = null!;

        [MaxLength(512)]
        public string Description { get; set; } = string.Empty;

        public float Price { get; set; }

        public IEnumerable<ListItemTimeSlotResponse> TimeSlots { get; set; }
    }
}
