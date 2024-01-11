namespace POS.Api.Models.DTOs.TimeSlot
{
    public class TimeSlotRequest
    {
        public Guid? Id { get; set; }

        public Guid ServiceId { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly End { get; set; }
    }
}
