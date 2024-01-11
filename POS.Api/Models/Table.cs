namespace POS.Api.Models
{
    public enum TableStatus
    {
        Free,
        Taken,
        Reserved
    }

    public class Table
    {
        public Guid Id { get; set; }

        public int Seats { get; set; }

        public TableStatus Status { get; set; }

        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
