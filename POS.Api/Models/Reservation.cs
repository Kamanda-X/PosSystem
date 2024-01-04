using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POS.Api.Models
{
    [PrimaryKey(nameof(TableId), nameof(OrderId))]
    public class Reservation
    {
        public Guid TableId { get; set; }

        public virtual Table Table { get; set; } = null!;

        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;

        public DateTimeOffset ReservedDate { get; set; }
    }
}
