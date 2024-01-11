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

        public Guid Id { get; init; }

        [ForeignKey("Employee")]
        public string EmployeeId { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;

        [MaxLength(512)]
        public string Description { get; set; } = string.Empty;

        public float Price { get; set; }

        public virtual Reservation? Reservation { get; set; }

        public ICollection<TimeSlot> TimeSlots { get; set; }
    }
}
