namespace POS.Api.Models
{
    public class TimeSlot
    {
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }

        public TimeOnly Start {  get; set; }

        public TimeOnly End { get; set; }

        public virtual Service Service { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
