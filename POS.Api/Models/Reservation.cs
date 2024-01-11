using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Api.Models
{
    [PrimaryKey(nameof(Id))]
    public class Reservation
    {
        public Guid Id { get; set; }

        public Guid? TableId { get; set; }

        public Guid? TimeSlotId { get; set; }

        public virtual TimeSlot TimeSlot { get; set; } = null!;

        public virtual Table Table { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
