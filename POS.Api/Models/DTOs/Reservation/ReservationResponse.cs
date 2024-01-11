using POS.Api.Models.DTOs.TimeSlot;

namespace POS.Api.Models.DTOs.Reservation
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }

        public Guid? TableId { get; set; }

        public ListItemTimeSlotResponse? TimeSlot { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
