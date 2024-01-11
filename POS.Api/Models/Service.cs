using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Api.Models
{
    public class Service
    {
        public Service()
        {
            TimeSlots = new HashSet<TimeSlot>();
        }

        [MaxLength(100)]
        public required string Name { get; set; }

        public Guid Id { get; init; }

        [ForeignKey("Employee")]
        public string EmployeeId { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;

        [MaxLength(512)]
        public required string Description { get; set; } = string.Empty;

        public required float Price { get; set; }

        public ICollection<TimeSlot> TimeSlots { get; set; }
    }
}
