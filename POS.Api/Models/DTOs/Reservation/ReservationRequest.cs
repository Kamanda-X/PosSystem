namespace POS.Api.Models.DTOs.Reservation
{
    public class ReservationRequest
    {
        public Guid? Id { get; set; }

        public Guid? TableId { get; set; }

        public Guid? TimeSlotId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
