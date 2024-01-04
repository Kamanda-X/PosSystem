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

        public virtual Reservation? Reservation { get; set; }
    }
}
